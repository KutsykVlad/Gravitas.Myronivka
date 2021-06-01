using System;
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
        // private readonly Stopwatch sw = new Stopwatch();

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
                    // sw.Reset();
                    // sw.Start();
                    using (var context = new GravitasDbContext())
                    {
                        _context = context;
                        Process();
                    }
                    // sw.Stop();
                    // if (sw.ElapsedMilliseconds > 20000)
                        // Logger.Info($"ProcessLoop processing time measure: NodeId = {_nodeId}, State = {_nodeDto.Context.OpRoutineStateId}, Time = {sw.Elapsed}");
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
            var validTill = DateTime.Now.AddMinutes(-1);
            var toDelete = _context.NodeProcessingMessages
                .Where(x => x.NodeId == _nodeId && x.DateTime < validTill)
                .ToList();
            if (toDelete.Any())
            {
                _context.NodeProcessingMessages.RemoveRange(toDelete);
                _context.SaveChanges();
            }
            
            SignalRInvoke.UpdateProcessingMessage(_nodeId);
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