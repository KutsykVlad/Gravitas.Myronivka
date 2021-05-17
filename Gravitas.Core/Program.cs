using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Web;
using Gravitas.Core.Manager;
using Gravitas.DAL.DeviceTransfer;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.Dto;
using Newtonsoft.Json;
using NLog;
using Topshelf;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Core
{
    public class Service
    {
        private readonly IOpRoutineCoreManager _opRoutineCoreManager;
        private readonly IDeviceSyncManager _deviceSyncManager;
        private readonly IQueueManager _queueManager;
        private readonly WebServiceHost _host = new WebServiceHost(typeof(ServiceApi), new Uri("http://localhost:8090/DeviceSync/"));

        public Service()
        {
            _opRoutineCoreManager = DependencyResolverConfig.Resolve<IOpRoutineCoreManager>();
            _queueManager = DependencyResolverConfig.Resolve<IQueueManager>();
            _deviceSyncManager = DependencyResolverConfig.Resolve<IDeviceSyncManager>();
        }

        public void Start()
        {
            _deviceSyncManager.StartDevSyncTasks();
            try
            {
                _host.Open();
            }
            catch (CommunicationException cex)
            {
                LogManager.GetCurrentClassLogger().Error("An exception occurred in Connect service: {0}", cex.Message);
                _host.Abort();
            }

            _queueManager.RestoreState();
            _opRoutineCoreManager.StartOpRoutineTasks();
            _opRoutineCoreManager.StartDisplayTasks();
        }

        public void Stop()
        {
            _opRoutineCoreManager.StopTasks();
        }

        [ServiceContract]
        public interface IService
        {
            [OperationContract]
            [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
            DeviceStateTransfer GetState(long deviceId);

            [OperationContract]
            [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
            void SetState(long deviceId, string outJson);
        }

        private class ServiceApi : IService
        {
            public DeviceStateTransfer GetState(long deviceId)
            {
                if (!Program.Devices.ContainsKey(deviceId)) return null;

                var state = Program.DeviceStates[deviceId];
                return new DeviceStateTransfer
                {
                    Id = deviceId,
                    DeviceType = Program.Devices[deviceId].TypeId,
                    ErrorCode = state.ErrorCode,
                    InData = state.InData,
                    LastUpdate = state.LastUpdate,
                    OutData = state.OutData
                };
            }

            public void SetState(long deviceId, string outJson)
            {
                if (!Program.Devices.ContainsKey(deviceId)) return;

                Program.DeviceStates[deviceId].OutData = outJson;
                Program.DeviceStates[deviceId].LastUpdate = DateTime.Now;
                Program.DeviceStates[deviceId].ErrorCode = 0;
            }
        }
    }

    static class Program
    {
        public static readonly ConcurrentDictionary<long, Device> Devices = new ConcurrentDictionary<long, Device>();
        public static readonly ConcurrentDictionary<long, DeviceState> DeviceStates = new ConcurrentDictionary<long, DeviceState>();
        public static readonly ConcurrentDictionary<long, DeviceParam> DeviceParams = new ConcurrentDictionary<long, DeviceParam>();

        public static void SetDeviceOutData(long deviceId, bool outData)
        {
            if (!Devices.ContainsKey(deviceId)) return;

            var data = JsonConvert.SerializeObject(new
            {
                Value = outData
            });
            
            DeviceStates[deviceId].OutData = data;
            DeviceStates[deviceId].LastUpdate = DateTime.Now;
            DeviceStates[deviceId].ErrorCode = 0;
        }
        
        public static BaseDeviceState GetDeviceState(long deviceId)
        {
            if (!DeviceStates.TryGetValue(deviceId, out var dev))
            {
                Console.WriteLine($"!!!!!!!!No device with id: {deviceId}");
                return null;
            }
            switch (Devices[deviceId].TypeId)
            {
                case Dom.Device.Type.RelayVkmodule4In0Out:
                    return new VkModuleI4O0State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = VkModuleI4O0InJsonState.FromJson(dev.InData),
                        OutData = VkModuleI4O0OutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.RelayVkmodule2In2Out:
                    return new VkModuleI2O2State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = VkModuleI2O2InJsonState.FromJson(dev.InData),
                        OutData = VkModuleI2O2OutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.DigitalIn:
                    return new DigitalInState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = DigitalInJsonState.FromJson(dev.InData),
                        OutData = null
                    };
                case Dom.Device.Type.DigitalOut:
                    return new DigitalOutState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = null,
                        OutData = DigitalOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.RfidObidRw:
                    return new RfidObidRwState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = RfidObidRwInJsonState.FromJson(dev.InData),
                        OutData = RfidObidRwOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.RfidZebraFx9500Antenna:
                    return new RfidZebraFx9500AntennaState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = RfidZebraFx9500AntennaInJsonState.FromJson(dev.InData),
                        OutData = RfidZebraFx9500AntennaOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.ScaleMettlerPT6S3:
                    return new ScaleState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = ScaleInJsonState.FromJson(dev.InData),
                        OutData = ScaleOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.LabFoss:
                    return new LabFossState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = LabFossInJsonState.FromJson(dev.InData),
                        OutData = LabFossOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.LabBruker:
                    return new LabBrukerState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = LabBrukerInJsonState.FromJson(dev.InData),
                        OutData = LabBrukerOutJsonState.FromJson(dev.OutData)
                    };
                case Dom.Device.Type.LabInfrascan:
                    return new LabInfrascanState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = LabInfrascanInJsonState.FromJson(dev.InData),
                        OutData = LabInfrascanOutJsonState.FromJson(dev.OutData)
                    };
                default: return null;
            }
        }

        static void Main()
        {
            Bootstrapper.Initialize();

            var rc = HostFactory.Run(x =>
            {
                x.Service<Service>(s =>
                {
                    s.ConstructUsing(name => new Service());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Gravitas.Core");
                x.SetDisplayName("Gravitas.Core");
                x.SetServiceName("Gravitas.Core");
                x.OnException(ex =>
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error($"Exception in Core: {ex.Message}. Stack trace: {ex.StackTrace}");
                });
                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(1); // restart the service after 1 minute
                });
            });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}