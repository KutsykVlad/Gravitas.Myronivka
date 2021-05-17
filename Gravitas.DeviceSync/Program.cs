using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.ServiceModel.Web;
using Gravitas.DAL.DeviceTransfer;
using Gravitas.DeviceSync.Manager;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Model;
using NLog;
using Topshelf;

namespace Gravitas.DeviceSync
{
    public class Service
    {
        private readonly IDeviceSyncManager _deviceSyncManager;
        private readonly WebServiceHost _host = new WebServiceHost(typeof(ServiceApi), new Uri("http://localhost:8090/DeviceSync/"));

        public Service()
        {
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
                LogManager.GetCurrentClassLogger().Error($"An exception occurred in Connect service: {0}", cex.Message);
                _host.Abort();
            }
        }

        public void Stop()
        {
            _deviceSyncManager.StopDevSyncTasks();
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

    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static readonly ConcurrentDictionary<long, Device> Devices = new ConcurrentDictionary<long, Device>();
        public static readonly ConcurrentDictionary<long, DeviceState> DeviceStates = new ConcurrentDictionary<long, DeviceState>();
        public static readonly ConcurrentDictionary<long, DeviceParam> DeviceParams = new ConcurrentDictionary<long, DeviceParam>();

        private static void Main()
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

                x.SetDescription("Gravitas.DeviceSync");
                x.SetDisplayName("Gravitas.DeviceSync");
                x.SetServiceName("Gravitas.DeviceSync");
                x.UseNLog(Logger.Factory);
            });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}