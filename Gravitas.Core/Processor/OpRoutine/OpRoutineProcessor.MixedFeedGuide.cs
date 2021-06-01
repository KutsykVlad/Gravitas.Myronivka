using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class MixedFeedGuideOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly IConnectManager _connectManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly ITicketRepository _ticketRepository;
        private readonly IQueueManager _queueManager;
        private readonly IUserManager _userManager;

        public MixedFeedGuideOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IConnectManager connectManager, 
            IRoutesInfrastructure routesInfrastructure, 
            IQueueManager queueManager, 
            IUserManager userManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository
                )
        {
            _ticketRepository = ticketRepository;
            _connectManager = connectManager;
            _routesInfrastructure = routesInfrastructure;
            _queueManager = queueManager;
            _userManager = userManager;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.MixedFeedGuide.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedGuide.State.BindLoadPoint:
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedGuide.State.AddOpVisa:
                    AddOperationVisa(NodeDetails);
                    break;
            }
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null
                || nodeDetailsDto.Context?.TicketId == null
                || nodeDetailsDto.Context?.OpDataId == null)
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
           
            var mixedFeedGuideOpData = _context.LoadGuideOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (mixedFeedGuideOpData == null) return;

            var ticket = _context.Tickets.First(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            
            var isNotFirstTicket = _context.Tickets.AsNoTracking().Any(x => 
                    x.TicketContainerId == nodeDetailsDto.Context.TicketContainerId
                    && (x.StatusId == TicketStatus.Completed || x.StatusId == TicketStatus.Closed)
                    && x.OrderNo < ticket.OrderNo);

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Призначена точка завантаження",
                LoadGuideOpDataId = mixedFeedGuideOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedGuide.State.AddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);
            
            if (!isNotFirstTicket && _opDataRepository.GetFirstOrDefault<SecurityCheckInOpData, Guid>(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value) == null)
            {
                _queueManager.OnImmediateEntranceAccept(nodeDetailsDto.Context.TicketContainerId.Value);
            }
            
            mixedFeedGuideOpData.StateId = OpDataState.Processed;
            _ticketRepository.Update<LoadGuideOpData, Guid>(mixedFeedGuideOpData);
            
            _connectManager.SendSms(SmsTemplate.DestinationPointApprovalSms, nodeDetailsDto.Context.TicketId);

            var nextNodes = _routesInfrastructure.GetNextNodes(ticket.Id);
            if (!isNotFirstTicket && nextNodes.Contains((int) NodeIdValue.MixedFeedGuide))
            {
                _routesInfrastructure.MoveForward(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id);
            }
            
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedGuide.State.Idle;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}