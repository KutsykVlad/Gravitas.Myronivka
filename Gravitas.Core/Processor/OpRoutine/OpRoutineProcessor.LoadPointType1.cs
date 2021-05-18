using System;
using System.Data.Entity;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.Dto;
using Dom = Gravitas.Model.DomainValue.Dom;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class LoadPointType1OpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly INodeManager _nodeManager;
        private readonly IRoutesManager _routesManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IUserManager _userManager;
        private readonly ILoadPointManager _loadPointManager;
        private readonly ICardManager _cardManager;

        public LoadPointType1OpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager, 
            INodeManager nodeManager, 
            IRoutesInfrastructure routesInfrastructure, 
            IUserManager userManager,
            ILoadPointManager loadPointManager, 
            ICardManager cardManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _nodeManager = nodeManager;
            _routesInfrastructure = routesInfrastructure;
            _userManager = userManager;
            _loadPointManager = loadPointManager;
            _cardManager = cardManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader)
                            && config.Rfid.ContainsKey(Dom.Node.Config.Rfid.OnGateReader);

            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.LoadPointType1.State.Workstation:
                    CheckForTruck(_nodeDto);
                    break;
                case Dom.OpRoutine.LoadPointType1.State.Idle:
                    CheckForTruck(_nodeDto);
                    break;
                case Dom.OpRoutine.LoadPointType1.State.GetTareValue:
                    break;
                case Dom.OpRoutine.LoadPointType1.State.AddOperationVisa:
                    AddOperationVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa:
                    AddChangeStateVisa(_nodeDto);
                    break;
            }

            if (_nodeDto.Config.DO.ContainsKey(Dom.Node.Config.DO.EmergencyOff)) CheckForInactive(_nodeDto);
        }
        
        private void CheckForInactive(Node nodeDto)
        {
            var turnOffOut = nodeDto.Config.DO[Dom.Node.Config.DO.EmergencyOff];
            var emergencyOffState = (DigitalOutState) Program.GetDeviceState(turnOffOut.DeviceId);
            if (emergencyOffState.OutData == null) emergencyOffState.OutData = new DigitalOutJsonState();
         
            Program.SetDeviceOutData(turnOffOut.DeviceId, nodeDto.IsActive && nodeDto.Context.TicketId.HasValue);
        }
        
        private void CheckForTruck(Node nodeDto)
        {
            if (nodeDto.Context.TicketContainerId.HasValue || !nodeDto.IsActive)
            {
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) return;
            
            var loadGuide = _opDataRepository.GetLastOpData<LoadGuideOpData>(card.Ticket.Id, Dom.OpDataState.Processed);
            var loadPoint = _opDataRepository.GetLastOpData<LoadPointOpData>(card.Ticket.Id, Dom.OpDataState.Processed);
            if (loadGuide is null || (loadPoint != null && loadGuide.CheckOutDateTime < loadPoint.CheckOutDateTime))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Info, $"Автомобілю не призначена точка завантаження"));
                return;
            }

            if (loadGuide.LoadPointNodeId != nodeDto.Id)
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage( 
                    _context.Nodes.Where(x => x.OrganisationUnitId == nodeDto.OrganisationUnitId)
                        .Select(x => x.Id)
                        .ToList(), 
                    new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Info,
                    $"Автомобілю який на {_nodeManager.GetNodeName(nodeDto.Id)} призначена точка завантаження " +
                    $"{_nodeManager.GetNodeName(loadGuide.LoadPointNodeId)}"));
                return;
            }
            
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            var loadPointOpData = new LoadPointOpData
            {
                StateId = Dom.OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.ContainerId
            };
            _ticketRepository.Add<LoadPointOpData, Guid>(loadPointOpData);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle;
            nodeDto.Context.TicketContainerId = card.Ticket.ContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = loadPointOpData.Id;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);

            if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto?.Context.TicketId == null || nodeDto.Context.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var loadPointResult = _loadPointManager.ConfirmLoadPoint(nodeDto.Context.TicketId.Value, card.EmployeeId);
            if (!loadPointResult) return;
            
            var loadPointOpData = _context.LoadPointOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == nodeDto.Context.TicketId.Value)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (loadPointOpData?.StateId == Dom.OpDataState.Rejected)
            {
                _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, nodeDto.Id, Dom.Route.Type.Reject);
            }
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Workstation;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
        }
        
        private void AddChangeStateVisa(Node nodeDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Вузол {nodeDto.Name} деактивовано",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            _nodeManager.ChangeNodeState(nodeDto.Id, false);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Workstation;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}