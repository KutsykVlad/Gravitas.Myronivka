using System;
using Gravitas.DeviceMonitor.Manager;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using NLog;
using Topshelf;

namespace Gravitas.DeviceMonitor
{
    public class Service
    {
        private readonly IDeviceMonitorManager _deviceSyncManager;

        public Service()
        {
            _deviceSyncManager = DependencyResolverConfig.Resolve<IDeviceMonitorManager>();
        }

        public void Start() => _deviceSyncManager.StartTasks();

        public void Stop() => _deviceSyncManager.StopTasks();
    }

    internal static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            Bootstrapper.Bootstrapper.Initialize();

            var rc = HostFactory.Run(x =>
            {
                x.Service<Service>(s =>
                {
                    s.ConstructUsing(name => new Service());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Gravitas.DeviceMonitor");
                x.SetDisplayName("Gravitas.DeviceMonitor");
                x.SetServiceName("Gravitas.DeviceMonitor");
                x.UseNLog(Logger.Factory);
            });

            var exitCode = (int) Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}