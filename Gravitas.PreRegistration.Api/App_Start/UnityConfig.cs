using Gravitas.DAL;
using Gravitas.DAL.Repository.PreRegistration;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Platform.Web.Manager.Report;
using Gravitas.PreRegistration.Api.Helpers;
using Gravitas.PreRegistration.Api.Helpers.Abstractions;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Unity;
using IQueueManager = Gravitas.PreRegistration.Api.Managers.Queue.IQueueManager;
using QueueManager = Gravitas.PreRegistration.Api.Managers.Queue.QueueManager;

namespace Gravitas.PreRegistration.Api
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterSingleton<ISettings, Settings>();
            container.RegisterType<GravitasDbContext, GravitasDbContext>();
            container.RegisterType<ICardRepository, CardRepository>();
            container.RegisterType<IOrganizationUnitRepository, OrganizationUnitRepository>();
            container.RegisterType<IOpDataRepository, OpDataRepository>();
            container.RegisterType<INodeRepository, NodeRepository>();
            container.RegisterType<IExternalDataRepository, ExternalDataRepository>();
            container.RegisterType<ITicketRepository, TicketRepository>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IQueueSettingsRepository, QueueSettingsRepository>();
            container.RegisterType<ITrafficRepository, TrafficRepository>();
            container.RegisterType<ISmsTemplatesRepository, SmsTemplatesRepository>();
            container.RegisterType<IOpDataManager, OpDataManager>();
            container.RegisterType<ICameraManager, CameraManager>();
            container.RegisterInstance<IMessageClient>(
                new MessageClient(GlobalConfigurationManager.EmailHost, GlobalConfigurationManager.RootEmail,
                GlobalConfigurationManager.RootEmailDestination,
                GlobalConfigurationManager.RootEmailLogin, GlobalConfigurationManager.RootEmailPassword));
            container.RegisterType<IConnectManager, ConnectManager>();
            container.RegisterType<IOpRoutineManager, OpRoutineManager>();
            container.RegisterType<IRoutesManager, RoutesManager>();
            container.RegisterType<IRoutesRepository, RoutesRepository>();
            container.RegisterType<IReportTool, ReportTool>();
            container.RegisterType<IPhonesRepository, PhonesRepository>();
            container.RegisterType<IRoutesInfrastructure, RoutesInfrastructure>();
            container.RegisterType<IQueueRegisterRepository, QueueRegisterRepository>();
            container.RegisterType<IQueueInfrastructure, QueueInfrastructure>();
            container.RegisterType<IPreRegistrationRepository, PreRegistrationRepository>();
            container.RegisterType<IQueueManager, QueueManager>();
            container.RegisterType<IEmailHelper, EmailHelper>();

            return container;
        }
    }
}