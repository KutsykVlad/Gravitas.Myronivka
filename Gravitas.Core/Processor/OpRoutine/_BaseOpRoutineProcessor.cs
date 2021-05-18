using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.Dto;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor
{
    public abstract class BaseOpRoutineProcessor : IOpRoutineProcessor
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected IDeviceManager _deviceManager;
        protected IDeviceRepository _deviceRepository;
        protected Node _nodeDto;

        protected long _nodeId;
        protected INodeRepository _nodeRepository;
        protected IOpDataRepository _opDataRepository;

        protected IOpRoutineManager _opRoutineManager;
        public GravitasDbContext _context;
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

        public abstract bool ValidateNodeConfig(NodeConfig config);

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

        public virtual void Config(long nodeId)
        {
            _nodeId = nodeId;
        }

        public virtual void ReadDbData()
        {
            _nodeDto = _nodeRepository.GetNodeDto(_nodeId);
        }

        protected bool UpdateNodeContext(long nodeId, NodeContext newContext, bool reload = true)
        {
            CheckProcessingTime(newContext);

            var result = _nodeRepository.UpdateNodeContext(nodeId, newContext, Dom.OpRoutine.Processor.CoreService);

            if (!result)
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error, "Не валідна спроба зміни стану вузла."));

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
                    StateId = Dom.OpDataState.Processed,
                    Message = "Час опрацювання на вузлі перевищило допустимий ліміт"
                };
                _opDataRepository.Add<NonStandartOpData, Guid>(standardOpData);

                _processingTimeError = true;
            }
        }

        public virtual bool ValidateNode(Node nodeDto)
        {
            if (nodeDto == null)
            {
                Logger.Warn("Core. Node detail is null.");
                return false;
            }

            if (nodeDto.OpRoutineId == null)
            {
                Logger.Warn($"Core. Node routine id is null.\nNode: {nodeDto.Id}");
                return false;
            }

            if (nodeDto.Context == null)
            {
                Logger.Warn($"Core. Node context is null.\nNode: {nodeDto.Id}");
                return false;
            }

            if (nodeDto.Context.OpRoutineStateId == null)
            {
                Logger.Warn($"Core. Node routine state id is null.\nNode: {nodeDto.Id}");
                return false;
            }

            if (nodeDto.Config == null)
            {
                Logger.Warn($"Core. Node config is null.\nNode: {nodeDto.Id}");
                return false;
            }

            if (!ValidateNodeConfig(nodeDto.Config))
            {
                Logger.Warn($"Core. Node device config is not valid.\nNode: {nodeDto.Id}");
                return false;
            }

            return true;
        }
    }
}