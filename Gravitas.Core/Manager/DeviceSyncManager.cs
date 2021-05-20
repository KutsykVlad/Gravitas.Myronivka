using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.Core.Manager.LabBruker;
using Gravitas.Core.Manager.LabFoss;
using Gravitas.Core.Manager.LabFoss2;
using Gravitas.Core.Manager.LabInfrascan;
using Gravitas.Core.Manager.RfidObidRwAutoAnswer;
using Gravitas.Core.Manager.RfidZebraFx9500;
using Gravitas.Core.Manager.ScaleMettlerPT6S3;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainValue;
using NLog;
using Unity.Resolution;
using DeviceType = Gravitas.Model.DomainValue.DeviceType;

namespace Gravitas.Core.Manager
{
    internal class DeviceSyncManager : IDeviceSyncManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DeviceRepository _deviceRepository;
        private readonly GravitasDbContext _context;
        private IDictionary<long, Task> _taskDictionary;
        private readonly CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        public DeviceSyncManager(DeviceRepository deviceRepository, GravitasDbContext context)
        {
            _deviceRepository = deviceRepository;
            _context = context;
        }

        public void StartDevSyncTasks()
        {
            var devList = _context.Devices.Where(x => x.IsActive).ToList();
            Logger.Info($"Device sync tasks to be started: {devList.Count}");

            foreach (var device in devList)
            {
                Program.Devices.TryAdd(device.Id, device);
            }

            var devIds = devList.Select(x => x.Id).Distinct().ToList();
            var deviceParams = _context.DeviceParams
                .Where(x => devIds.Any(z => z == x.Id))
                .ToList();
            foreach (var param in deviceParams)
            {
                Program.DeviceParams.TryAdd(param.Id, param);
            }

            foreach (var id in Program.DeviceParams)
            {
                Program.DeviceStates.TryAdd(id.Key, new DeviceState
                {
                    Id = id.Key,
                    InData = null,
                    OutData = null,
                    ErrorCode = (int) DeviceErrorCode.NA
                });
            }

            _taskDictionary = new Dictionary<long, Task>();
            foreach (var dev in devList)
            {
                var task = GetTask(dev, cancelTokenSource.Token);
                if (task == null) continue;
                task.Start();

                Logger.Info(
                    $"{_taskDictionary.Count + 1:D3}/{devList.Count():D3} Sync task for \"{dev.Id}-{dev.Name}\" is starting..");

                _taskDictionary.Add(dev.Id, task);
            }
        }

        public void StopDevSyncTasks()
        {
            foreach (var state in Program.DeviceStates)
            {
                _deviceRepository.Update<DeviceState, int>(state.Value);
            }
            foreach (var value in _taskDictionary.Values)
                Logger.Info(
                    $"Device Task Id: {value.Id}, should be stopped soon.");

            var time = DateTime.Now;

            while (!IsAllFinished() || (DateTime.Now - time).TotalSeconds < 30)
            {
                Logger.Warn(
                    $"{DateTime.Now}, one or more Tasks is still running after stop");
                Thread.Sleep(1000);
            }
        }

        public bool IsAllFinished()
        {
            return _taskDictionary.Select(e => e.Value)
                .Any(task => task != null && (task.IsCompleted || task.IsCanceled || task.IsFaulted));
        }

        private Task GetTask(Device device, CancellationToken token)
        {
            Task task = null;
            switch (device.TypeId)
            {
                case DeviceType.RfidObidRw:
                    var obidManager = DependencyResolverConfig.Resolve<IRfidObidRwManager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(() => obidManager.SyncData(token));
                    break;
                case DeviceType.RfidZebraFx9500Head:
                    var zebraManager = DependencyResolverConfig.Resolve<IRfidZebraFx9500Manager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(() => zebraManager.SyncData(token));
                    break;
                case DeviceType.RelayVkmodule2In2Out:
                    var socket2Manager = DependencyResolverConfig.Resolve<VkModuleSocket2.IVkModuleSocket2Manager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(() => socket2Manager.SyncData(token));
                    break;
                case DeviceType.RelayVkmodule4In0Out:
                    var socket1Manager = DependencyResolverConfig.Resolve<VkModuleSocket1.IVkModuleSocket1Manager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(() => socket1Manager.SyncData(token));
                    break;
                case DeviceType.ScaleMettlerPT6S3:
                    var mettlerPT6S3Manager = DependencyResolverConfig.Resolve<IScaleMettlerPT6S3Manager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(() => mettlerPT6S3Manager.SyncData(token));
                    break;
                case DeviceType.LabBruker:
                    var LabBrukerManager = DependencyResolverConfig.Resolve<ILabBrukerManager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(LabBrukerManager.SyncData, TaskCreationOptions.LongRunning);
                    break;
                case DeviceType.LabFoss:
                    var labFossManager = DependencyResolverConfig.Resolve<ILabFossManager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(labFossManager.SyncData, TaskCreationOptions.LongRunning);
                    break;
                case DeviceType.LabFoss2:
                    var labFossManager2 = DependencyResolverConfig.Resolve<ILabFossManager2>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(labFossManager2.SyncData, TaskCreationOptions.LongRunning);
                    break;
                case DeviceType.LabInfrascan:
                    var InfrascanManager = DependencyResolverConfig.Resolve<ILabInfrascanManager>(
                        new ParameterOverride("deviceId", device.Id));
                    task = new Task(InfrascanManager.SyncData, TaskCreationOptions.LongRunning);
                    break;
            }

            return task;
        }
    }
}