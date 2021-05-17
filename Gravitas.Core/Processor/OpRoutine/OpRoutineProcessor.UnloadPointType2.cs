using System;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.Dto;
using Dom = Gravitas.Model.DomainValue.Dom;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

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

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;
            
            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.OnGateReader);

            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.UnloadPointType2.State.Workstation:
                    Idle(_nodeDto);
                    break;
                case Dom.OpRoutine.UnloadPointType2.State.Idle:
                    Idle(_nodeDto);
                    break;      
                case Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint:
                    break;
                case Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa:
                    AddOperationVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa:
                    AddChangeStateVisa(_nodeDto);
                    break;
            }
        }

        private void Idle(Node nodeDto)
        {
            if (nodeDto.Context.TicketContainerId.HasValue || !nodeDto.IsActive)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Info,
                    $"Вузол {nodeDto.Id} не активний або зайнятий."));
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card?.Ticket == null) return;
            
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDto);
            
            var unloadPointOpData = new UnloadPointOpData
            {
                StateId = Dom.OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now
            };
            _ticketRepository.Add<UnloadPointOpData, Guid>(unloadPointOpData);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Idle;
            nodeDto.Context.TicketContainerId = card.Ticket.ContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = unloadPointOpData.Id;

            if (!UpdateNodeContext(nodeDto.Id, nodeDto.Context)) return;

            if (nodeDto.OrganisationUnitId.HasValue)
                SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDto);
            if (card == null) return;
            
            var unloadResult = _unloadPointManager.ConfirmUnloadPoint(nodeDto.Context.TicketId.Value, card.EmployeeId);
            if (!unloadResult) return;
            
            _routesInfrastructure.MoveForward(nodeDto.Context.TicketId.Value, nodeDto.Id);
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            if (nodeDto.OrganisationUnitId.HasValue)
                SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value); 
        }
        
        private void AddChangeStateVisa(Node nodeDto)
        {
            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Вузол {nodeDto.Name} деактивовано",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            _nodeManager.ChangeNodeState(nodeDto.Id, false);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}