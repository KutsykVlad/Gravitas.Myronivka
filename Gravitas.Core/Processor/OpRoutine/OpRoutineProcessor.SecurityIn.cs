using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class SecurityInOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICameraManager _cameraManager;
        private readonly ICardManager _cardManager;
        private readonly ICardRepository _cardRepository;
        private readonly IQueueManager _queue;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ITicketRepository _ticketRepository;

        public SecurityInOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ICardRepository cardRepository,
            ICameraManager cameraManager,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            IQueueManager queue,
            IRoutesInfrastructure routesInfrastructure,
            ICardManager cardManager, 
            IUserManager userManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _cardRepository = cardRepository;
            _cameraManager = cameraManager;
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _queue = queue;
            _routesInfrastructure = routesInfrastructure;
            _cardManager = cardManager;
            _userManager = userManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.OnGateReader);
            var cameraValid = config.Camera.ContainsKey(Dom.Node.Config.Camera.Camera1);

            return rfidValid && cameraValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.SecurityIn.State.Idle:
                    WatchBarrier(_nodeDto);
                    Idle(_nodeDto);
                    break;
                case Dom.OpRoutine.SecurityIn.State.CheckOwnTransport:
                    WatchBarrier(_nodeDto);
                    break;
                case Dom.OpRoutine.SecurityIn.State.BindLongRangeRfid:
                    WatchBarrier(_nodeDto);
                    BindLongRangeRfid(_nodeDto);
                    break;
                case Dom.OpRoutine.SecurityIn.State.AddOperationVisa:
                    WatchBarrier(_nodeDto);
                    AddOperationVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.SecurityIn.State.OpenBarrier:
                    OpenBarrier(_nodeDto);
                    break;
                case Dom.OpRoutine.SecurityIn.State.GetCamSnapshot:
                    WatchBarrier(_nodeDto);
                    GetCamSnapshot(_nodeDto);
                    break;
            }
        }

        private void Idle(Node nodeDto)
        {
            if (nodeDto.Context == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"Хибний контекст вузла"));
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) return;

            if (card.IsOwn)
            {
                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa;
                nodeDto.Context.OpProcessData =
                    _cardRepository.GetFirstOrDefault<OwnTransport, long>(x => x.CardId == card.Id)?.Id;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }
            var supplyCode =_context.SingleWindowOpDatas.Where(x => x.TicketId == card.Ticket.Id)
                                                               .Select(c => c.SupplyCode)
                                                               .FirstOrDefault();

            if (supplyCode != Dom.SingleWindowOpData.TechnologicalSupplyCode && card.Ticket.StatusId != Dom.Ticket.Status.Processing)
            {
                if (!_queue.IsAllowedEnterTerritory(card.Ticket.Id))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                        new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"Не маєте права заходити без виклику по черзі."));

                    _cardManager.SetRfidValidationDO(false, nodeDto);
                    return;
                }
            }

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                        new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, errorMessage));

                    _cardManager.SetRfidValidationDO(false, nodeDto);
                return;
                }

            var securityCheckInOpData = new SecurityCheckInOpData
            {
                StateId = Dom.OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.ContainerId
            };
            _ticketRepository.Add<SecurityCheckInOpData, Guid>(securityCheckInOpData);
            
            card.Ticket.StatusId = Dom.Ticket.Status.Processing;
            _ticketRepository.Update<Ticket, long>(card.Ticket);
            _routesInfrastructure.MoveForward(card.Ticket.Id, nodeDto.Id);
            if (_routesInfrastructure.IsRouteWithoutGuide(card.Ticket.Id))
            {
                _routesInfrastructure.AddDestinationOpData(card.Ticket.Id, nodeDto.Id);
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.BindLongRangeRfid;
            nodeDto.Context.TicketContainerId = card.Ticket.ContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = securityCheckInOpData.Id;
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void WatchBarrier(Node nodeDto)
        {
            if (!nodeDto.Config.DI.ContainsKey(Dom.Node.Config.DI.Barrier)) return;
            
            var iBarrierConfig = nodeDto.Config.DI[Dom.Node.Config.DI.Barrier];
            var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, iBarrierState, TimeSpan.FromSeconds(3)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return ;
            }

            if (iBarrierState.InData.Value != true) return;

            var isNewEntryNeeded = !_context.NonStandartOpDatas
                .AsNoTracking()
                .Where(e => e.NodeId == nodeDto.Id && e.CheckInDateTime != null)
                .AsEnumerable()
                .Any(e =>
                    (DateTime.Now - e.CheckInDateTime.Value).TotalSeconds <
                    GlobalConfigurationManager.NonStandartPassTimeout);

            if (!isNewEntryNeeded) return;

            _opRoutineManager.LogNonStandardOp(nodeDto, new NonStandartOpData
            {
                NodeId = nodeDto.Id,
                CheckInDateTime = DateTime.Now,
                StateId = Dom.OpDataState.Processed,
                Message = "Несанкціонований проїзд"
            });
        }

        private void BindLongRangeRfid(Node nodeDto)
        {
            if (nodeDto.IsEmergency) return;

            if (!nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader))
            {
                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }

            var validCardId = GetValidZebraCard(nodeDto);
            if (validCardId == null) return;

            var card = _context.Cards.FirstOrDefault(x => x.Id == validCardId);
            if (card == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"Картку не знайдено."));
                return;
            }

            if (card.TicketContainerId.HasValue)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "Використано існуючу мітку"));
                return;
            }
            
            _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Success, "Мітка знаходиться в радіусі дії. Виконується перевірка."));
            
            for (var i = 1; i <= 10; i++)
            {
                Thread.Sleep(1000);
                var tempCard = GetValidZebraCard(nodeDto);
                if (tempCard == null || card.Id != tempCard) { return; }
            }

            card.TicketContainerId = nodeDto.Context.TicketContainerId;
            _cardRepository.Update<Card, string>(card);

            _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Success, "Нову мітку прив'язано"));

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void AddOperationVisa(Node nodeDto)
        {
//            if (nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDto);
            if (card == null) return;

            if (!nodeDto.Context.OpProcessData.HasValue)
            {
                var securityCheckInOpData = _context.SecurityCheckInOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
                if (securityCheckInOpData == null) return;

                var visa = new OpVisa
                {
                    DateTime = DateTime.Now,
                    Message = "Дозвіл на заїзд",
                    SecurityCheckInOpDataId = securityCheckInOpData.Id,
                    EmployeeId = card.EmployeeId,
                    OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa
                };

                _nodeRepository.Add<OpVisa, long>(visa);
                _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
                securityCheckInOpData.StateId = Dom.OpDataState.Processed;
                securityCheckInOpData.CheckOutDateTime = DateTime.Now;
                _ticketRepository.Update<SecurityCheckInOpData, Guid>(securityCheckInOpData);
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.OpenBarrier;
            nodeDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void OpenBarrier(Node nodeDto)
        {
            if (!nodeDto.Config.DO.ContainsKey(Dom.Node.Config.DO.Barrier) )
            {
                if (nodeDto.Id == (long)NodeIdValue.SecurityIn2)
                {
                    var cameraImagesList = _cameraManager.GetSnapshots(nodeDto.Config);
                    foreach (var camImageId in cameraImagesList)
                    {
                        var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                        camImage.SecurityCheckInOpDataId = nodeDto.Context.OpDataId;
                        _context.SaveChanges();
                    }
                }

                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.GetCamSnapshot;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }

            var iBarrierConfig = nodeDto.Config.DI[Dom.Node.Config.DI.Barrier];
            var oBarrierConfig = nodeDto.Config.DO[Dom.Node.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);

            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            while (true)
            {
                Program.SetDeviceOutData(oBarrierState.Id, true);

                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    var cameraImagesList = _cameraManager.GetSnapshots(nodeDto.Config);
                    foreach (var camImageId in cameraImagesList)
                    {
                        var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                        camImage.SecurityCheckInOpDataId = nodeDto.Context.OpDataId;
                        _context.SaveChanges();
                    }
                    break;
                }
                Thread.Sleep(1000);
            }

            oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);
            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            while (true)
            {
                Program.SetDeviceOutData(oBarrierState.Id, false);

                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == false) break;
                Thread.Sleep(1000);
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.GetCamSnapshot;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void GetCamSnapshot(Node nodeDto)
        {
            if (nodeDto?.Context == null) return;
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            Thread.Sleep(2000);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        private string GetValidZebraCard(Node nodeDto)
        {
            var rfidConfig = nodeDto.Config.Rfid[Dom.Node.Config.Rfid.LongRangeReader];

            var rfidObidRwState = (RfidZebraFx9500AntennaState) Program.GetDeviceState(rfidConfig.DeviceId);
            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, rfidObidRwState))
            {
                if (!string.IsNullOrEmpty(errMsgItem?.Text)) _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return null;
            }

            var sortCardIds = rfidObidRwState.InData.TagList
                                               .Where(e => _cardRepository.GetFirstOrDefault<Card, string>(card => card.Id == e.Key) != null)
                                               .Select(e => e);
            var readerCardIds = sortCardIds
                                    .Where(e => (DateTime.Now - e.Value).TotalSeconds <= rfidConfig.Timeout)
                                    .OrderByDescending(e => e.Value)
                                    .Select(e => e.Key)
                                    .ToList();

            if (!readerCardIds.Any())
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "В радіусі дії зчитувача міток не виявлено"));
                return null;
            }
            
            if (readerCardIds.Count > 1)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "В радіусі дії зчитувача більше 1 мітки"));
                return null;
            }

            var containerCardIds = _context.Cards.AsNoTracking().Where(e => e.TicketContainerId == nodeDto.Context.TicketContainerId
                                                                && e.IsActive
                                                                && e.TypeId == Dom.Card.Type.TransportCard)
                                   .Select(e => e.Id)
                                   .ToList();

            var validCardIds = containerCardIds.Any()
                ? readerCardIds.Where(e => containerCardIds.Contains(e)).ToList()
                : readerCardIds.Where(item => _cardRepository.GetFirstOrDefault<Card, string>(card => card.Id == item) != null).ToList();

            if (validCardIds.Count < 1)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                        $@"В радіусі дії зчитувача міток, що підходять для опрацювання не виявлено. Всього виявлено міток {readerCardIds.Count}"));
                return null;
            }

            return validCardIds.First();
        }
    }
}