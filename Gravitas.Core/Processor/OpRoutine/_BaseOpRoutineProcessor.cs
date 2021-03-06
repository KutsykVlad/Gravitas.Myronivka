using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Core.Processor.OpRoutine
{
    public abstract class BaseOpRoutineProcessor : IOpRoutineProcessor
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected List<NodeProcessingMessage> Messages = new List<NodeProcessingMessage>();
        protected readonly IDeviceManager _deviceManager;
        protected readonly IDeviceRepository _deviceRepository;
        protected NodeDetails NodeDetails;

        protected int _nodeId;
        protected INodeRepository _nodeRepository;
        protected IOpDataRepository _opDataRepository;

        protected IOpRoutineManager _opRoutineManager;
        protected GravitasDbContext _context;
        private DateTime? _processingStartedAt;
        private bool _processingTimeError;

        protected BaseOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository)
        {
            _opRoutineManager = opRoutineManager;
            _deviceManager = deviceManager;
            _deviceRepository = deviceRepository;
            _nodeRepository = nodeRepository;
            _opDataRepository = opDataRepository;
        }

        public async Task ProcessLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var context = new GravitasDbContext())
                    {
                        _context = context;
                        Process();
                    }
                }
                catch (Exception e)
                {
                    e.Data.Add("NodeId", _nodeId);
                    Logger.Error(e);
                }

                await Task.Delay(1000, token);
            }
        }

        public abstract void Process();

        public virtual void Config(int nodeId)
        {
            _nodeId = nodeId;
        }

        public virtual void ReadDbData()
        {
            NodeDetails = _nodeRepository.GetNodeDto(_nodeId);
            UpdateProcessingMessages();
        }
        
        private void UpdateProcessingMessages()
        {
            var newMessages = _context.NodeProcessingMessages
                .Where(x => x.NodeId == _nodeId)
                .ToList();

            if (ShouldBeUpdated(newMessages, Messages))
            {
                Messages = newMessages;
                var validTill = DateTime.Now.AddMinutes(-1);
                var toDelete = Messages
                    .Where(x => x.DateTime < validTill)
                    .ToList();
                Messages = Messages.Except(toDelete).ToList();
                if (toDelete.Any())
                {
                    _context.NodeProcessingMessages.RemoveRange(toDelete);
                    _context.SaveChanges();
                }

                if (Messages.Any())
                {
                    SignalRInvoke.UpdateProcessingMessage(_nodeId);
                }
            }
        }

        private bool ShouldBeUpdated(List<NodeProcessingMessage> newMessages, List<NodeProcessingMessage> messages)
        {
            if (newMessages.Count != messages.Count)
            {
                return true;
            }
            foreach (var newMessage in newMessages)
            {
                if (messages.All(x => x.Message != newMessage.Message
                                      && x.NodeId != newMessage.NodeId 
                                      && x.DateTime != newMessage.DateTime))
                {
                    return true;
                }
            }

            return false;
        }

        protected bool UpdateNodeContext(int nodeId, NodeContext newContext, bool reload = true)
        {
            CheckProcessingTime(newContext);

            var result = _nodeRepository.UpdateNodeContext(nodeId, newContext);

            if (!result)
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(
                    ProcessingMsgType.Error, "Не валідна спроба зміни стану вузла."));

            if (result && reload)
            {
                SignalRInvoke.ReloadHubGroup(nodeId);
            }

            return result;
        }
        
        private void CheckProcessingTime(NodeContext newContext)
        {
            var currentDto = _nodeRepository.GetNodeDto(_nodeId);
            if (!currentDto.Context.TicketId.HasValue && newContext.TicketId.HasValue)
            {
                _processingStartedAt = DateTime.Now;
            }
            
            if (currentDto.Context.TicketId.HasValue && !newContext.TicketId.HasValue)
            {
                _processingStartedAt = null;
                _processingTimeError = false;
            }

            if (!_processingTimeError 
                && _processingStartedAt.HasValue 
                && (DateTime.Now - _processingStartedAt.Value).Minutes > currentDto.MaximumProcessingTime)
            {
                var standardOpData = new NonStandartOpData
                {
                    NodeId = currentDto.Id,
                    TicketContainerId = currentDto.Context.TicketContainerId,
                    TicketId = currentDto.Context.TicketId,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    StateId = OpDataState.Processed,
                    Message = "Час опрацювання на вузлі перевищило допустимий ліміт"
                };
                _opDataRepository.Add<NonStandartOpData, Guid>(standardOpData);

                _processingTimeError = true;
            }
        }
    }
}