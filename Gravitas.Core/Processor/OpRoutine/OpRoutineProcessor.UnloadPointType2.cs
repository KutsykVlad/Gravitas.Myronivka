using System;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class UnloadPointType2OpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly INodeManager _nodeManager;
        private readonly IRoutesManager _routesManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly ICardManager _cardManager;
        private readonly IUserManager _userManager;
        private readonly IUnloadPointManager _unloadPointManager;

        public UnloadPointType2OpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            INodeManager nodeManager, 
            IRoutesInfrastructure routesInfrastructure, 
            ICardManager cardManager, 
            IUserManager userManager,
            IUnloadPointManager unloadPointManager) :
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
            _cardManager = cardManager;
            _userManager = userManager;
            _unloadPointManager = unloadPointManager;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.UnloadPointType2.State.Workstation:
                    Idle(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType2.State.Idle:
                    Idle(NodeDetails);
                    break;      
                case Model.DomainValue.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType2.State.AddOperationVisa:
                    AddOperationVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType2.State.AddChangeStateVisa:
                    AddChangeStateVisa(NodeDetails);
                    break;
            }
        }

        private void Idle(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context.TicketContainerId.HasValue || !nodeDetailsDto.IsActive)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Info,
                    $"Вузол {nodeDetailsDto.Id} не активний або зайнятий."));
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card?.Ticket == null) return;
            
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);
            
            var unloadPointOpData = new UnloadPointOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now
            };
            _ticketRepository.Add<UnloadPointOpData, Guid>(unloadPointOpData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType2.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = unloadPointOpData.Id;

            if (!UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context)) return;

            if (nodeDetailsDto.OrganisationUnitId.HasValue)
                SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;
            
            var unloadResult = _unloadPointManager.ConfirmUnloadPoint(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId.Value);
            if (!unloadResult) return;
            
            _routesInfrastructure.MoveForward(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id);
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType2.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            if (nodeDetailsDto.OrganisationUnitId.HasValue)
                SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value); 
        }
        
        private void AddChangeStateVisa(NodeDetails nodeDetailsDto)
        {
            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Вузол {nodeDetailsDto.Name} деактивовано",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType2.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeManager.ChangeNodeState(nodeDetailsDto.Id, false);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType2.State.Idle;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}