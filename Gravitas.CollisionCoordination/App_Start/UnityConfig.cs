using System.Web.Http;
using Gravitas.CollisionCoordination.Manager.LaboratoryData;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.Repository.Sms;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.DAL.Repository.Traffic;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.ReportTool;
using Gravitas.Infrastructure.Platform.Manager.Routes;
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