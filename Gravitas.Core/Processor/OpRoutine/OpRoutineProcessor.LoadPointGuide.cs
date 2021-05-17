using System;
using System.Data.Entity;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

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
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.LoadPointGuide.State.Idle:
                    break;
                case Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint:
                    break;
                case Dom.OpRoutine.LoadPointGuide.State.AddOpVisa:
                    AddOperationVisa(_nodeDto);
                    break;
            }
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketContainerId == null || nodeDto.Context.TicketId == null) return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
            if (ticket == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null || !_cardManager.IsMasterEmployeeCard(card, nodeDto.Id)) return;
               
            var isNotFirstTicket =
                _context.Tickets.AsNoTracking().Any(x => 
                    x.ContainerId == nodeDto.Context.TicketContainerId
                    && (x.StatusId == Dom.Ticket.Status.Completed || x.StatusId == Dom.Ticket.Status.Closed)
                    && x.OrderNo < ticket.OrderNo);
            
            if (nodeDto.Context.OpProcessData.HasValue && nodeDto.Context.OpProcessData == (long) NodeIdValue.UnloadPointGuideEl23)
            {
                var unloadResultConfirm = _unloadPointManager.ConfirmUnloadGuide(nodeDto.Context.TicketId.Value, card.EmployeeId);
                if (!unloadResultConfirm) return;
            }
            else
            {
                var loadResultConfirm = _loadPointManager.ConfirmLoadGuide(nodeDto.Context.TicketId.Value, card.EmployeeId);
                if (!loadResultConfirm) return;
                var wasOnSecurity = _opDataRepository.GetFirstOrDefault<SecurityCheckInOpData, Guid>(x => x.TicketId == ticket.Id) != null;
                
                if (!wasOnSecurity && !isNotFirstTicket)
                {
                    _queueManager.OnImmediateEntranceAccept(nodeDto.Context.TicketContainerId.Value);
                }
            }
            
            _connectManager.SendSms(Dom.Sms.Template.DestinationPointApprovalSms, nodeDto.Context.TicketId);
         
            if (ticket.RouteItemIndex == 0 || (ticket.SecondaryRouteTemplateId.HasValue && ticket.SecondaryRouteItemIndex == 0))
            {
                _routesInfrastructure.MoveForward(ticket.Id, nodeDto.Id);
            }

            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}