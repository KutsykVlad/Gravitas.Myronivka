using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Web;
using Gravitas.Core.Manager;
using Gravitas.DAL.DeviceTransfer;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;
using NLog;
using Topshelf;

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
            DeviceStateTransfer GetState(int deviceId);

            [OperationContract]
            [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
            void SetState(int deviceId, string outJson);
        }

        private class ServiceApi : IService
        {
            public DeviceStateTransfer GetState(int deviceId)
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

            public void SetState(int deviceId, string outJson)
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
        public static readonly ConcurrentDictionary<int, Device> Devices = new ConcurrentDictionary<int, Device>();
        public static readonly ConcurrentDictionary<int, DeviceState> DeviceStates = new ConcurrentDictionary<int, DeviceState>();
        public static readonly ConcurrentDictionary<int, DeviceParam> DeviceParams = new ConcurrentDictionary<int, DeviceParam>();

        public static void SetDeviceOutData(int deviceId, bool outData)
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
        
        public static BaseDeviceState GetDeviceState(int deviceId)
        {
            if (!DeviceStates.TryGetValue(deviceId, out var dev))
            {
                Console.WriteLine($"!!!!!!!!No device with id: {deviceId}");
                return null;
            }
            switch (Devices[deviceId].TypeId)
            {
                case Model.DomainValue.DeviceType.RelayVkmodule4In0Out:
                    return new VkModuleI4O0State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<VkModuleI4O0InJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<VkModuleI4O0OutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.RelayVkmodule2In2Out:
                    return new VkModuleI2O2State
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<VkModuleI2O2InJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<VkModuleI2O2OutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.DigitalIn:
                    return new DigitalInState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<DigitalInJsonState>(dev.InData),
                        OutData = null
                    };
                case Model.DomainValue.DeviceType.DigitalOut:
                    return new DigitalOutState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = null,
                        OutData = JsonConvert.DeserializeObject<DigitalOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.RfidObidRw:
                    return new RfidObidRwState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<RfidObidRwInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<RfidObidRwOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.RfidZebraFx9500Antenna:
                    return new RfidZebraFx9500AntennaState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<RfidZebraFx9500AntennaInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<RfidZebraFx9500AntennaOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.ScaleMettlerPT6S3:
                    return new ScaleState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<ScaleInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<ScaleOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.LabFoss:
                    return new LabFossState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabFossInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabFossOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.LabFoss2:
                    return new LabFossState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabFossInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabFossOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.LabBruker:
                    return new LabBrukerState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabBrukerInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabBrukerOutJsonState>(dev.OutData)
                    };
                case Model.DomainValue.DeviceType.LabInfrascan:
                    return new LabInfrascanState
                    {
                        Id = dev.Id,
                        ErrorCode = dev.ErrorCode,
                        LastUpdate = dev.LastUpdate,
                        InData = JsonConvert.DeserializeObject<LabInfrascanInJsonState>(dev.InData),
                        OutData = JsonConvert.DeserializeObject<LabInfrascanOutJsonState>(dev.OutData)
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