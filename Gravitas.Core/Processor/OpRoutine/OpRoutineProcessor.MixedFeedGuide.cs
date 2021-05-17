using System;
using System.Data.Entity;
using System.Linq;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

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

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            return config != null && config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader);
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.MixedFeedGuide.State.Idle:
                    break;
                case Dom.OpRoutine.MixedFeedGuide.State.BindLoadPoint:
                    break;
                case Dom.OpRoutine.MixedFeedGuide.State.AddOpVisa:
                    AddOperationVisa(_nodeDto);
                    break;
            }
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context?.TicketId == null
                || nodeDto.Context?.OpDataId == null)
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
           
            var mixedFeedGuideOpData = _context.MixedFeedGuideOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (mixedFeedGuideOpData == null) return;

            var ticket = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value);
            
            var isNotFirstTicket = _context.Tickets.AsNoTracking().Any(x => 
                    x.ContainerId == nodeDto.Context.TicketContainerId
                    && (x.StatusId == Dom.Ticket.Status.Completed || x.StatusId == Dom.Ticket.Status.Closed)
                    && x.OrderNo < ticket.OrderNo);

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Призначена точка завантаження",
                MixedFeedGuideOpDataId = mixedFeedGuideOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.MixedFeedGuide.State.AddOpVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);
            
            if (!isNotFirstTicket && _opDataRepository.GetFirstOrDefault<SecurityCheckInOpData, Guid>(x => x.TicketId == nodeDto.Context.TicketId.Value) == null)
            {
                _queueManager.OnImmediateEntranceAccept(nodeDto.Context.TicketContainerId.Value);
            }
            
            mixedFeedGuideOpData.StateId = Dom.OpDataState.Processed;
            _ticketRepository.Update<MixedFeedGuideOpData, Guid>(mixedFeedGuideOpData);
            
            _connectManager.SendSms(Dom.Sms.Template.DestinationPointApprovalSms, nodeDto.Context.TicketId);

            var nextNodes = _routesInfrastructure.GetNextNodes(ticket.Id);
            if (!isNotFirstTicket && nextNodes.Contains((long) NodeIdValue.MixedFeedGuide))
            {
                _routesInfrastructure.MoveForward(nodeDto.Context.TicketId.Value, nodeDto.Id);
            }
            
            nodeDto.Context.TicketId = null;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedGuide.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}