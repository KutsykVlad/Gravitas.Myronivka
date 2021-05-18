using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.Core.Processor;
using Gravitas.Core.Processor.OpRoutine;
using Gravitas.Core.Processor.QueueDisplay;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Node.DAO;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Core.Manager
{
    internal class OpRoutineCoreManager : IOpRoutineCoreManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

        private readonly INodeRepository _nodeRepository;
        private IDictionary<long, Task> _taskDictionary;

        public OpRoutineCoreManager(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public void StartOpRoutineTasks()
        {
            IEnumerable<Node> nodeList = _nodeRepository.GetQuery<Node, long>();

            Logger.Info($"Node routine tasks to be started: {nodeList.Count()}");

            _taskDictionary = new Dictionary<long, Task>();
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
                    _nodeRepository.GetQuery<Device, long>().Where(t => t.IsActive && t.TypeId == Dom.Device.Type.Display);

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
                case Dom.OpRoutine.SingleWindow.Id:
                    processor = DependencyResolverConfig.Resolve<SingleWindowOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.SecurityIn.Id:
                    processor = DependencyResolverConfig.Resolve<SecurityInOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.SecurityOut.Id:
                    processor = DependencyResolverConfig.Resolve<SecurityOutOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.SecurityReview.Id:
                    processor = DependencyResolverConfig.Resolve<SecurityReviewOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.LabolatoryIn.Id:
                    processor = DependencyResolverConfig.Resolve<LaboratoryInOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.Weighbridge.Id:
                    processor = DependencyResolverConfig.Resolve<WeighbridgeOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.UnloadPointGuide.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointGuideOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.UnloadPointType1.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointType1OpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.LoadPointType1.Id:
                    processor = DependencyResolverConfig.Resolve<LoadPointType1OpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.UnloadPointType2.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointType2OpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.LoadCheckPoint.Id:
                    processor = DependencyResolverConfig.Resolve<LoadCheckPointOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.UnloadCheckPoint.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadCheckPointOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.LoadPointGuide.Id:
                    processor = DependencyResolverConfig.Resolve<LoadPointGuideOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.CentralLaboratorySamples.Id:
                    processor = DependencyResolverConfig.Resolve<CentralLaboratorySamplesOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.Id:
                    processor = DependencyResolverConfig.Resolve<CentralLaboratoryProcessOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.MixedFeedManage.Id:
                    processor = DependencyResolverConfig.Resolve<MixedFeedManageOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.MixedFeedGuide.Id:
                    processor = DependencyResolverConfig.Resolve<MixedFeedGuideOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.MixedFeedLoad.Id:
                    processor = DependencyResolverConfig.Resolve<MixedFeedLoadOpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.UnloadPointGuide2.Id:
                    processor = DependencyResolverConfig.Resolve<UnloadPointGuide2OpRoutineProcessor>();
                    break;
                case Dom.OpRoutine.LoadPointGuide2.Id:
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

        private Task GetDisplayTask(long displayId, CancellationToken token)
        {
            IQueueDisplayProcessor processor = DependencyResolverConfig.Resolve<QueueDisplayProcessor>();
            processor.Config(displayId);
            return new Task(() => processor.ProcessLoop(token));
        }
    }
}