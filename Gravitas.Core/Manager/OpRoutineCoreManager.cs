using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.Core.Processor.OpRoutine;
using Gravitas.Core.Processor.QueueDisplay;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainValue;
using NLog;
using DeviceType = Gravitas.Model.DomainValue.DeviceType;

namespace Gravitas.Core.Manager
{
    internal class OpRoutineCoreManager : IOpRoutineCoreManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

        private readonly INodeRepository _nodeRepository;
        private readonly GravitasDbContext _context;
        private IDictionary<int, Task> _taskDictionary;

        public OpRoutineCoreManager(INodeRepository nodeRepository, GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _context = context;
        }

        public void StartOpRoutineTasks()
        {
            IEnumerable<Node> nodeList = _context.Nodes.ToList();

            Logger.Info($"Node routine tasks to be started: {nodeList.Count()}");

            _taskDictionary = new Dictionary<int, Task>();
            foreach (var node in nodeList)
            {
                var task = GetTask(node, _cancelTokenSource.Token);
                if (task == null) continue;

                task.Start();

                Logger.Info($"{_taskDictionary.Count + 1:D3}/{nodeList.Count():D3} Routine task for \"{node.Id}-{node.Name}\" is starting..");

                _taskDictionary.Add(node.Id, task);
            }
        }

        public void StartDisplayTasks()
        {
            if (_taskDictionary != null)
            {
                IEnumerable<Device> displayList =
                    _nodeRepository.GetQuery<Device, int>().Where(t => t.IsActive && t.TypeId == DeviceType.Display);

                Logger.Info($"Display tasks to be started: {displayList.Count()}");
                foreach (var display in displayList)
                {
                    var task = GetDisplayTask(display.Id, _cancelTokenSource.Token);
                    if (task == null) continue;

                    task.Start();

                    Logger.Info(
                        $"{_taskDictionary.Count + 1:D3}/{displayList.Count():D3} Display task for \"{display.Id}-{display.Name}\" is starting..");

                    _taskDictionary.Add(display.Id, task);
                }
            }
        }

        public void StopTasks()
        {
            foreach (var value in _taskDictionary.Values)
                Logger.Info($"Task Id: {value.Id}, should be stopped soon.");

            _cancelTokenSource.Cancel();

            while (!IsAllFinished())
            {
                Logger.Info($"{DateTime.Now}, one or more Tasks is still running after stop");
                Thread.Sleep(1000);
            }
        }
        
        public bool IsAllFinished()
        {
            return _taskDictionary.Select(e => e.Value)
                .Any(task => task != null && (task.IsCompleted || task.IsCanceled || task.IsFaulted));
        }

        private Task GetTask(Node node, CancellationToken token)
        {
            IOpRoutineProcessor processor;
            switch (node.OpRoutineId)
            {
                case OpRoutine.SingleWindow.Id:
                    processor = DependencyResolverConfig.Resolve<SingleWindowOpRoutineProcessor>();
                    break;
                case OpRoutine.SecurityIn.Id:
                    processor = DependencyResolverConfig.Resolve<SecurityInOpRoutineProcessor>();
                    break;
                case OpRoutine.SecurityOut.Id:
                    processor = DependencyResolverConfig.Resolve<SecurityOutOpRoutineProcessor>();
                    break;
                case OpRoutine.SecurityReview.Id:
                    processor = DependencyResolverConfig.Resolve<SecurityReviewOpRoutineProcessor>();
                    break;
                case OpRoutine.LaboratoryIn.Id:
                    processor = DependencyResolverConfig.Resolve<LaboratoryInOpRoutineProcessor>();
                    break;
                case OpRoutine.Weighbridge.Id:
                    processor = DependencyResolverConfig.Resolve<WeighbridgeOpRoutineProcessor>();
                    break;
                case OpRoutine.UnloadPointGuide.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointGuideOpRoutineProcessor>();
                    break;
                case OpRoutine.UnloadPointType1.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointType1OpRoutineProcessor>();
                    break;
                case OpRoutine.LoadPointType1.Id:
                    processor = DependencyResolverConfig.Resolve<LoadPointType1OpRoutineProcessor>();
                    break;
                case OpRoutine.UnloadPointType2.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointType2OpRoutineProcessor>();
                    break;
                case OpRoutine.LoadCheckPoint.Id:
                    processor = DependencyResolverConfig.Resolve<LoadCheckPointOpRoutineProcessor>();
                    break;
                case OpRoutine.UnloadCheckPoint.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadCheckPointOpRoutineProcessor>();
                    break;
                case OpRoutine.LoadPointGuide.Id:
                    processor = DependencyResolverConfig.Resolve<LoadPointGuideOpRoutineProcessor>();
                    break;
                case OpRoutine.CentralLaboratorySamples.Id:
                    processor = DependencyResolverConfig.Resolve<CentralLaboratorySamplesOpRoutineProcessor>();
                    break;
                case OpRoutine.CentralLaboratoryProcess.Id:
                    processor = DependencyResolverConfig.Resolve<CentralLaboratoryProcessOpRoutineProcessor>();
                    break;
                case OpRoutine.MixedFeedManage.Id:
                    processor = DependencyResolverConfig.Resolve<MixedFeedManageOpRoutineProcessor>();
                    break;
                case OpRoutine.MixedFeedGuide.Id:
                    processor = DependencyResolverConfig.Resolve<MixedFeedGuideOpRoutineProcessor>();
                    break;
                case OpRoutine.MixedFeedLoad.Id:
                    processor = DependencyResolverConfig.Resolve<MixedFeedLoadOpRoutineProcessor>();
                    break;
                case OpRoutine.UnloadPointGuide2.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointGuide2OpRoutineProcessor>();
                    break;
                case OpRoutine.LoadPointGuide2.Id:
                    processor = DependencyResolverConfig.Resolve<LoadPointGuide2OpRoutineProcessor>();
                    break;
                default:
                    processor = null;
                    break;
            }

            if (processor == null) throw new NotImplementedException("OpRoutine is not implemented");

            processor.Config(node.Id);
            return new Task(async () => await processor.ProcessLoop(token));
        }

        private Task GetDisplayTask(int displayId, CancellationToken token)
        {
            IQueueDisplayProcessor processor = DependencyResolverConfig.Resolve<QueueDisplayProcessor>();
            processor.Config(displayId);
            return new Task(() => processor.ProcessLoop(token));
        }
    }
}