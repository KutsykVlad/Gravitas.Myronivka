using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class SecurityOutOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICameraManager _cameraManager;
        private readonly ICardManager _cardManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ITicketRepository _ticketRepository;

        public SecurityOutOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ICameraManager cameraManager,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            IRoutesInfrastructure routesInfrastructure,
            ICardManager cardManager, 
            IUserManager userManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _cameraManager = cameraManager;
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _routesInfrastructure = routesInfrastructure;
            _cardManager = cardManager;
            _userManager = userManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader)
                            && config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader);
            var cameraValid = config.Camera.ContainsKey(NodeData.Config.Camera.Camera1);

            return rfidValid && cameraValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.SecurityOut.State.Idle:
                    WatchBarrier(_nodeDto);
                    Idle(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.CheckOwnTransport:
                    WatchBarrier(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.ShowOperationsList:
                    WatchBarrier(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.EditStampList:
                    WatchBarrier(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.AddRouteControlVisa:
                    WatchBarrier(_nodeDto);
                    AddRouteControlVisa(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa:
                    WatchBarrier(_nodeDto);
                    AddTransportInspectionVisa(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.OpenBarrier:
                    OpenBarrier(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.SecurityOut.State.GetCamSnapshot:
                    WatchBarrier(_nodeDto);
                    GetCamSnapshot(_nodeDto);
                    break;
            }
        }

        private void Idle(Node nodeDto)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) return;

            if (card.IsOwn)
            {
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa;
                nodeDto.Context.OpProcessData =
                    _ticketRepository.GetFirstOrDefault<OwnTransport, int>(x => x.CardId == card.Id)?.Id;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            var securityCheckOutOpData = new SecurityCheckOutOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.TicketContainerId
            };
            _ticketRepository.Add<SecurityCheckOutOpData, Guid>(securityCheckOutOpData);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.ShowOperationsList;
            nodeDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = securityCheckOutOpData.Id;

            if (!UpdateNodeContext(nodeDto.Id, nodeDto.Context))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"Не валідна спроба зміни стану вузла."));
                return;
            }

            _cardManager.SetRfidValidationDO(true, nodeDto);

            _routesInfrastructure.MoveForward(card.Ticket.Id, nodeDto.Id);
        }

        private void WatchBarrier(Node nodeDto)
        {
            if (!nodeDto.Config.DO.ContainsKey(NodeData.Config.DI.Barrier)) return;

            var iBarrierConfig = nodeDto.Config.DI[NodeData.Config.DI.Barrier];
            var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, iBarrierState, TimeSpan.FromSeconds(3)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return;
            }

            if (iBarrierState.InData.Value != true) return;

            var isNewEntryNeeded = !_context.NonStandartOpDatas
                .AsNoTracking()
                .Where(e => e.NodeId == nodeDto.Id && e.CheckInDateTime != null)
                .AsEnumerable()
                .Any(e =>
                    (DateTime.Now - e.CheckInDateTime.Value).TotalSeconds < GlobalConfigurationManager.NonStandartPassTimeout);

            if (!isNewEntryNeeded) return;

            _opRoutineManager.LogNonStandardOp(nodeDto, new NonStandartOpData
            {
                NodeId = nodeDto.Id,
                CheckInDateTime = DateTime.Now,
                StateId = OpDataState.Processed,
                Message = "Несанкціонований проїзд"
            });
        }

        public void AddRouteControlVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var securityCheckOutOpData = _context.SecurityCheckOutOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (securityCheckOutOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Перевірка проходження маршруту",
                SecurityCheckOutOpDataId = securityCheckOutOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddRouteControlVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void AddTransportInspectionVisa(Node nodeDto)
        {
            //            if (nodeDto?.Context?.OpDataId == null || nodeDto.Context.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDto);
            if (card == null) return;

            if (!nodeDto.Context.OpProcessData.HasValue)
            {
                var securityCheckOutOpData = _context.SecurityCheckOutOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
                if (securityCheckOutOpData == null) return;
                securityCheckOutOpData.StateId = OpDataState.Processed;
                securityCheckOutOpData.CheckOutDateTime = DateTime.Now;
                _context.SaveChanges();

                var visa = new OpVisa
                {
                    DateTime = DateTime.Now,
                    Message = "Зовнишній огляд проведено",
                    SecurityCheckOutOpDataId = securityCheckOutOpData.Id,
                    EmployeeId = card.EmployeeId,
                    OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddTransportInspectionVisa
                };
                _nodeRepository.Add<OpVisa, int>(visa);

                var next = _routesInfrastructure.GetNextNodes(nodeDto.Context.TicketId.Value);

                if (next?.Count==1 && next.First()==nodeDto.Id)
                {
                    var ticket = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value);
                    ticket.StatusId = TicketStatus.Completed;
                    _context.SaveChanges();
                }
            }

            _cardManager.SetRfidValidationDO(true, nodeDto);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.OpenBarrier;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void OpenBarrier(Node nodeDto)
        {
            if (!nodeDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier))
            {
                if (nodeDto.Id == (long)NodeIdValue.SecurityOut2)
                {
                    var cameraImagesListBottom = _cameraManager.GetSnapshots(nodeDto.Config);
                    foreach (var camImageIdBottom in cameraImagesListBottom)
                    {
                        var camImage = _context.OpCameraImages.First(x => x.Id == camImageIdBottom);
                        camImage.SecurityCheckOutOpDataId = nodeDto.Context.OpDataId;
                        _context.SaveChanges();
                    }
                }

                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.GetCamSnapshot;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }

            //            if (nodeDto.Context?.OpDataId == null)
            //                return;

            var iBarrierConfig = nodeDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDto.Config.DO[NodeData.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);

            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            var cameraImagesList = _cameraManager.GetSnapshots(nodeDto.Config);
            foreach (var camImageId in cameraImagesList)
            {
                var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                camImage.SecurityCheckOutOpDataId = nodeDto.Context.OpDataId;
                _context.SaveChanges();
            }

            while (true)
            {
                Program.SetDeviceOutData(oBarrierState.Id, true);

                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
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

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.GetCamSnapshot;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void GetCamSnapshot(Node nodeDto)
        {
            Thread.Sleep(2000);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}