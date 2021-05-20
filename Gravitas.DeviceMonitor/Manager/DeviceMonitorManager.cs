using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.DAL.Repository.Node;
using Gravitas.DeviceMonitor.Monitor;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Model.DomainModel.Node.DAO;
using NLog;

namespace Gravitas.DeviceMonitor.Manager
{
    public class DeviceMonitorManager : IDeviceMonitorManager
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IDictionary<long, Task> _taskDictionary = new Dictionary<long, Task>();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

        public DeviceMonitorManager(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public void StartTasks()
        {
            var nodeList = _nodeRepository.GetQuery<Node, int>();

            foreach (var node in nodeList)
            {
                var task = GetTask(node, _cancelTokenSource.Token);
                task?.Start();

                _logger.Info($"{_taskDictionary.Count + 1:D3}/{nodeList.Count():D3} Monitor task for \"{node.Id}-{node.Name}\" is starting..");

                _taskDictionary.Add(node.Id, task);
            }
        }

        public void StopTasks()
        {
            _cancelTokenSource.Cancel();

            while (!IsAllFinished())
            {
                _logger.Info($"{DateTime.Now}, one or more Tasks is still running after stop");
                Thread.Sleep(1000);
            }
        }

        private bool IsAllFinished()
        {
            return _taskDictionary.Select(x => x.Value).All(x => x.IsCompleted || x.IsCanceled || x.IsFaulted);
        }
        
        private Task GetTask(Node node, CancellationToken token)
        {
            var monitor = DependencyResolverConfig.Resolve<DeviceIpMonitor>();

            monitor.Config(node.Id);
            return new Task(() => monitor.ProcessLoop(token));
        }
    }
}