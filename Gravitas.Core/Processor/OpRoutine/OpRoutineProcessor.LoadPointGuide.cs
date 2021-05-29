using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class LoadPointGuideOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly IConnectManager _connectManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IQueueManager _queueManager;
        private readonly IUserManager _userManager;
        private readonly ILoadPointManager _loadPointManager;
        private readonly IUnloadPointManager _unloadPointManager;
        private readonly ICardManager _cardManager;

        public LoadPointGuideOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            IConnectManager connectManager, 
            IRoutesInfrastructure routesInfrastructure,
            IQueueManager queueManager, 
            IUserManager userManager, 
            ILoadPointManager loadPointManager, 
            IUnloadPointManager unloadPointManager, 
            ICardManager cardManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository
                )
        {
            _connectManager = connectManager;
            _routesInfrastructure = routesInfrastructure;
            _queueManager = queueManager;
            _userManager = userManager;
            _loadPointManager = loadPointManager;
            _unloadPointManager = unloadPointManager;
            _cardManager = cardManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            return config != null;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(NodeDetailsDto)) return;

            switch (NodeDetailsDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.LoadPointGuide.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.LoadPointGuide.State.BindLoadPoint:
                    break;
                case Model.DomainValue.OpRoutine.LoadPointGuide.State.AddOpVisa:
                    AddOperationVisa(NodeDetailsDto);
                    break;
            }
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null || nodeDetailsDto.Context.TicketId == null) return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            if (ticket == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null || !_cardManager.IsMasterEmployeeCard(card, nodeDetailsDto.Id)) return;
               
            var isNotFirstTicket =
                _context.Tickets.AsNoTracking().Any(x => 
                    x.TicketContainerId == nodeDetailsDto.Context.TicketContainerId
                    && (x.StatusId == TicketStatus.Completed || x.StatusId == TicketStatus.Closed)
                    && x.OrderNo < ticket.OrderNo);
            
            if (nodeDetailsDto.Context.OpProcessData.HasValue && nodeDetailsDto.Context.OpProcessData == (long) NodeIdValue.UnloadPointGuideEl23)
            {
                var unloadResultConfirm = _unloadPointManager.ConfirmUnloadGuide(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId);
                if (!unloadResultConfirm) return;
            }
            else
            {
                var loadResultConfirm = _loadPointManager.ConfirmLoadGuide(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId);
                if (!loadResultConfirm) return;
                var wasOnSecurity = _opDataRepository.GetFirstOrDefault<SecurityCheckInOpData, Guid>(x => x.TicketId == ticket.Id) != null;
                
                if (!wasOnSecurity && !isNotFirstTicket)
                {
                    _queueManager.OnImmediateEntranceAccept(nodeDetailsDto.Context.TicketContainerId.Value);
                }
            }
            
            _connectManager.SendSms(SmsTemplate.DestinationPointApprovalSms, nodeDetailsDto.Context.TicketId);
         
            if (ticket.RouteItemIndex == 0 || (ticket.SecondaryRouteTemplateId.HasValue && ticket.SecondaryRouteItemIndex == 0))
            {
                _routesInfrastructure.MoveForward(ticket.Id, nodeDetailsDto.Id);
            }

            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointGuide.State.Idle;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}