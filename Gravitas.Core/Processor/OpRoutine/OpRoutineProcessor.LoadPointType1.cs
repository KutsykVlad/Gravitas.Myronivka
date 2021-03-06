using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

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

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.LoadPointType1.State.Workstation:
                    CheckForTruck(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LoadPointType1.State.Idle:
                    CheckForTruck(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LoadPointType1.State.GetTareValue:
                    break;
                case Model.DomainValue.OpRoutine.LoadPointType1.State.AddOperationVisa:
                    AddOperationVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LoadPointType1.State.AddChangeStateVisa:
                    AddChangeStateVisa(NodeDetails);
                    break;
            }

            if (NodeDetails.Config.DO.ContainsKey(NodeData.Config.DO.EmergencyOff)) CheckForInactive(NodeDetails);
        }
        
        private void CheckForInactive(NodeDetails nodeDetailsDto)
        {
            var turnOffOut = nodeDetailsDto.Config.DO[NodeData.Config.DO.EmergencyOff];
            var emergencyOffState = (DigitalOutState) Program.GetDeviceState(turnOffOut.DeviceId);
            if (emergencyOffState.OutData == null) emergencyOffState.OutData = new DigitalOutJsonState();
         
            Program.SetDeviceOutData(turnOffOut.DeviceId, nodeDetailsDto.IsActive && nodeDetailsDto.Context.TicketId.HasValue);
        }
        
        private void CheckForTruck(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context.TicketContainerId.HasValue || !nodeDetailsDto.IsActive)
            {
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;
            
            var loadGuide = _opDataRepository.GetLastOpData<LoadGuideOpData>(card.Ticket.Id, OpDataState.Processed);
            var loadPoint = _opDataRepository.GetLastOpData<LoadPointOpData>(card.Ticket.Id, OpDataState.Processed);
            if (loadGuide is null || (loadPoint != null && loadGuide.CheckOutDateTime < loadPoint.CheckOutDateTime))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Info, $"Автомобілю не призначена точка завантаження"));
                return;
            }

            if (loadGuide.LoadPointNodeId != nodeDetailsDto.Id)
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage( 
                    _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                        .Select(x => x.Id)
                        .ToList(), 
                    new NodeProcessingMsgItem(
                        ProcessingMsgType.Info,
                    $"Автомобілю який на {_nodeManager.GetNodeName(nodeDetailsDto.Id)} призначена точка завантаження " +
                    $"{_nodeManager.GetNodeName(loadGuide.LoadPointNodeId)}"));
                return;
            }
            
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            var loadPointOpData = new LoadPointOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.TicketContainerId
            };
            _ticketRepository.Add<LoadPointOpData, Guid>(loadPointOpData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointType1.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = loadPointOpData.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);

            if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context.TicketId == null || nodeDetailsDto.Context.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var loadPointResult = _loadPointManager.ConfirmLoadPoint(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId.Value);
            if (!loadPointResult) return;
            
            var loadPointOpData = _context.LoadPointOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (loadPointOpData?.StateId == OpDataState.Rejected)
            {
                _routesInfrastructure.SetSecondaryRoute(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id, RouteType.Reject);
            }
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointType1.State.Workstation;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
        }
        
        private void AddChangeStateVisa(NodeDetails nodeDetailsDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Вузол {nodeDetailsDto.Name} деактивовано",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointType1.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeManager.ChangeNodeState(nodeDetailsDto.Id, false);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointType1.State.Workstation;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}