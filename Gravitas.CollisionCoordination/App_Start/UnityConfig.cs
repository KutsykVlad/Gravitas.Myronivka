using System.Web.Http;
using Gravitas.CollisionCoordination.Manager.LaboratoryData;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Platform.Web.Manager.Report;
using Unity;
using Unity.AspNet.WebApi;
using CollisionManager = Gravitas.CollisionCoordination.Manager.CollisionManager.CollisionManager;
using ICollisionManager = Gravitas.CollisionCoordination.Manager.CollisionManager.ICollisionManager;

namespace Gravitas.CollisionCoordination
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
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
				new MessageClient (GlobalConfigurationManager.EmailHost, GlobalConfigurationManager.RootEmail,
				GlobalConfigurationManager.RootEmailDestination,
				GlobalConfigurationManager.RootEmailLogin, GlobalConfigurationManager.RootEmailPassword));
            container.RegisterType<IConnectManager, ConnectManager>();

			container.RegisterType<IOpRoutineManager, OpRoutineManager>();
			container.RegisterType<IRoutesManager, RoutesManager>();
			container.RegisterType<IRoutesRepository, RoutesRepository>();
			container.RegisterType<IReportTool, ReportTool>();
			container.RegisterType<ICollisionManager, CollisionManager>();
			container.RegisterType<ILaboratoryDataManager, LaboratoryDataManager>();
			container.RegisterType<IPhonesRepository, PhonesRepository>();
			container.RegisterType<IRoutesInfrastructure, RoutesInfrastructure>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}