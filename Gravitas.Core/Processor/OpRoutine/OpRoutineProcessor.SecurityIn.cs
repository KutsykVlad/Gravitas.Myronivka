using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using CardType = Gravitas.Model.DomainValue.CardType;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

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

            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader);
            var cameraValid = config.Camera.ContainsKey(NodeData.Config.Camera.Camera1);

            return rfidValid && cameraValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(NodeDetailsDto)) return;

            switch (NodeDetailsDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.SecurityIn.State.Idle:
                    WatchBarrier(NodeDetailsDto);
                    Idle(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityIn.State.CheckOwnTransport:
                    WatchBarrier(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityIn.State.BindLongRangeRfid:
                    WatchBarrier(NodeDetailsDto);
                    BindLongRangeRfid(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityIn.State.AddOperationVisa:
                    WatchBarrier(NodeDetailsDto);
                    AddOperationVisa(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityIn.State.OpenBarrier:
                    OpenBarrier(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityIn.State.GetCamSnapshot:
                    WatchBarrier(NodeDetailsDto);
                    GetCamSnapshot(NodeDetailsDto);
                    break;
            }
        }

        private void Idle(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"Хибний контекст вузла"));
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            if (card.IsOwn)
            {
                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.AddOperationVisa;
                nodeDetailsDto.Context.OpProcessData =
                    _cardRepository.GetFirstOrDefault<OwnTransport, int>(x => x.CardId == card.Id)?.Id;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }
            var supplyCode =_context.SingleWindowOpDatas.Where(x => x.TicketId == card.Ticket.Id)
                                                               .Select(c => c.SupplyCode)
                                                               .FirstOrDefault();

            if (supplyCode != TechRoute.SupplyCode && card.Ticket.StatusId != TicketStatus.Processing)
            {
                if (!_queue.IsAllowedEnterTerritory(card.Ticket.Id))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                        new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"Не маєте права заходити без виклику по черзі."));

                    _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                    return;
                }
            }

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));

                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
            return;
            }

            var securityCheckInOpData = new SecurityCheckInOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.TicketContainerId
            };
            _ticketRepository.Add<SecurityCheckInOpData, Guid>(securityCheckInOpData);
            
            card.Ticket.StatusId = TicketStatus.Processing;
            _ticketRepository.Update<Ticket, int>(card.Ticket);
            _routesInfrastructure.MoveForward(card.Ticket.Id, nodeDetailsDto.Id);
            if (_routesInfrastructure.IsRouteWithoutGuide(card.Ticket.Id))
            {
                _routesInfrastructure.AddDestinationOpData(card.Ticket.Id, nodeDetailsDto.Id);
            }

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.BindLongRangeRfid;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = securityCheckInOpData.Id;
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void WatchBarrier(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)) return;
            
            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];
            var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, iBarrierState, TimeSpan.FromSeconds(3)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return ;
            }

            if (iBarrierState.InData.Value != true) return;

            var isNewEntryNeeded = !_context.NonStandartOpDatas
                .AsNoTracking()
                .Where(e => e.NodeId == nodeDetailsDto.Id && e.CheckInDateTime != null)
                .AsEnumerable()
                .Any(e =>
                    (DateTime.Now - e.CheckInDateTime.Value).TotalSeconds <
                    GlobalConfigurationManager.NonStandartPassTimeout);

            if (!isNewEntryNeeded) return;

            _opRoutineManager.LogNonStandardOp(nodeDetailsDto, new NonStandartOpData
            {
                NodeId = nodeDetailsDto.Id,
                CheckInDateTime = DateTime.Now,
                StateId = OpDataState.Processed,
                Message = "Несанкціонований проїзд"
            });
        }

        private void BindLongRangeRfid(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.IsEmergency) return;

            if (!nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader))
            {
                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.AddOperationVisa;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }

            var validCardId = GetValidZebraCard(nodeDetailsDto);
            if (validCardId == null) return;

            var card = _context.Cards.FirstOrDefault(x => x.Id == validCardId);
            if (card == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"Картку не знайдено."));
                return;
            }

            if (card.TicketContainerId.HasValue)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Warning, "Використано існуючу мітку"));
                return;
            }
            
            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Success, "Мітка знаходиться в радіусі дії. Виконується перевірка."));
            
            for (var i = 1; i <= 10; i++)
            {
                Thread.Sleep(1000);
                var tempCard = GetValidZebraCard(nodeDetailsDto);
                if (tempCard == null || card.Id != tempCard) { return; }
            }

            card.TicketContainerId = nodeDetailsDto.Context.TicketContainerId;
            _cardRepository.Update<Card, string>(card);

            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Success, "Нову мітку прив'язано"));

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.AddOperationVisa;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        public void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
//            if (nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            if (!nodeDetailsDto.Context.OpProcessData.HasValue)
            {
                var securityCheckInOpData = _context.SecurityCheckInOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
                if (securityCheckInOpData == null) return;

                var visa = new OpVisa
                {
                    DateTime = DateTime.Now,
                    Message = "Дозвіл на заїзд",
                    SecurityCheckInOpDataId = securityCheckInOpData.Id,
                    EmployeeId = card.EmployeeId,
                    OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.AddOperationVisa
                };

                _nodeRepository.Add<OpVisa, int>(visa);
                _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
                securityCheckInOpData.StateId = OpDataState.Processed;
                securityCheckInOpData.CheckOutDateTime = DateTime.Now;
                _ticketRepository.Update<SecurityCheckInOpData, Guid>(securityCheckInOpData);
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.OpenBarrier;
            nodeDetailsDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void OpenBarrier(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier) )
            {
                if (nodeDetailsDto.Id == (long)NodeIdValue.SecurityIn2)
                {
                    var cameraImagesList = _cameraManager.GetSnapshots(nodeDetailsDto.Config);
                    foreach (var camImageId in cameraImagesList)
                    {
                        var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                        camImage.SecurityCheckInOpDataId = nodeDetailsDto.Context.OpDataId;
                        _context.SaveChanges();
                    }
                }

                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.GetCamSnapshot;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }

            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDetailsDto.Config.DO[NodeData.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);

            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            while (true)
            {
                Program.SetDeviceOutData(oBarrierState.Id, true);

                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    var cameraImagesList = _cameraManager.GetSnapshots(nodeDetailsDto.Config);
                    foreach (var camImageId in cameraImagesList)
                    {
                        var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                        camImage.SecurityCheckInOpDataId = nodeDetailsDto.Context.OpDataId;
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

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.GetCamSnapshot;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void GetCamSnapshot(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context == null) return;
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            Thread.Sleep(2000);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityIn.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        private string GetValidZebraCard(NodeDetails nodeDetailsDto)
        {
            var rfidConfig = nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.LongRangeReader];

            var rfidObidRwState = (RfidZebraFx9500AntennaState) Program.GetDeviceState(rfidConfig.DeviceId);
            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, rfidObidRwState))
            {
                if (!string.IsNullOrEmpty(errMsgItem?.Text)) _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
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
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Warning, "В радіусі дії зчитувача міток не виявлено"));
                return null;
            }
            
            if (readerCardIds.Count > 1)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Warning, "В радіусі дії зчитувача більше 1 мітки"));
                return null;
            }

            var containerCardIds = _context.Cards.AsNoTracking().Where(e => e.TicketContainerId == nodeDetailsDto.Context.TicketContainerId
                                                                && e.IsActive
                                                                && e.TypeId == CardType.TransportCard)
                                   .Select(e => e.Id)
                                   .ToList();

            var validCardIds = containerCardIds.Any()
                ? readerCardIds.Where(e => containerCardIds.Contains(e)).ToList()
                : readerCardIds.Where(item => _cardRepository.GetFirstOrDefault<Card, string>(card => card.Id == item) != null).ToList();

            if (validCardIds.Count < 1)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Warning,
                        $@"В радіусі дії зчитувача міток, що підходять для опрацювання не виявлено. Всього виявлено міток {readerCardIds.Count}"));
                return null;
            }

            return validCardIds.First();
        }
    }
}