using Gravitas.Core.DeviceManager.Card;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.Core.Manager;
using Gravitas.Core.Manager.LabBruker;
using Gravitas.Core.Manager.LabFoss;
using Gravitas.Core.Manager.LabFoss2;
using Gravitas.Core.Manager.LabInfrascan;
using Gravitas.Core.Manager.RfidObidRwAutoAnswer;
using Gravitas.Core.Manager.RfidZebraFx9500;
using Gravitas.Core.Manager.ScaleMettlerPT6S3;
using Gravitas.Core.Manager.VkModuleSocket1;
using Gravitas.Core.Manager.VkModuleSocket2;
using Gravitas.Core.Valves;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.EmployeeRoles;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.Repository.Sms;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.DAL.Repository.Traffic;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Display;
using Gravitas.Infrastructure.Platform.Manager.LoadPoint;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.OpVisa;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Infrastructure.Platform.Manager.ReportTool;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Unity;
using Unity.Injection;

namespace Gravitas.Core.DependencyInjection
{
	public class CoreTypeResolver : ITypeResolver
	{
		public void RegisterType(IUnityContainer container) {
			container.RegisterType<GravitasDbContext, GravitasDbContext>();
			container.RegisterSingleton<ISettings, Settings>();

			container.RegisterType<ICardRepository, CardRepository>();
			container.RegisterType<IValveService, ValveService>();

			container.RegisterType<INodeRepository, NodeRepository>();
			container.RegisterType<ITicketRepository, TicketRepository>();
		    container.RegisterType<ITrafficRepository, TrafficRepository>();

            container.RegisterType<IQueueSettingsRepository, QueueSettingsRepository>();

		    container.RegisterType<ISmsTemplatesRepository, SmsTemplatesRepository>();

            container.RegisterType<IOpDataRepository, OpDataRepository>();

			container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IPhoneInformTicketAssignmentRepository, PhoneInformTicketAssignmentRepository>();
            container.RegisterType<IPhonesRepository, PhonesRepository>();
			container.RegisterType<IOpRoutineManager, OpRoutineManager>();
			container.RegisterType<ICardManager, CardManager>();
			container.RegisterType<IDeviceSyncManager, DeviceSyncManager>();
			container.RegisterType<IDeviceManager, DeviceManager.Device.DeviceManager>();
			container.RegisterType<INodeManager, NodeManager>();
			container.RegisterType<ICameraManager, CameraManager>();

			container.RegisterInstance<IMessageClient>( 
				new MessageClient (GlobalConfigurationManager.EmailHost, GlobalConfigurationManager.RootEmail,
					GlobalConfigurationManager.RootEmailDestination,
					GlobalConfigurationManager.RootEmailLogin, GlobalConfigurationManager.RootEmailPassword));
		    container.RegisterType<IEmployeeRolesRepository, EmployeeRolesRepository>();
		    

            container.RegisterType<ISmsMobizonApiClient, SmsMobizonApiClient>(
				new InjectionConstructor(GlobalConfigurationManager.SmsApiHost, GlobalConfigurationManager.SmsApiToken));

		    container.RegisterType<IQueueDisplay, QueueDisplay>();
            container.RegisterType<IConnectManager, ConnectManager>();
		    container.RegisterType<IReportTool, ReportTool>();
            container.RegisterType<IOpRoutineCoreManager, OpRoutineCoreManager>();
			container.RegisterType<IRoutesManager, RoutesManager>();
			container.RegisterType<IRoutesRepository, RoutesRepository>();
			container.RegisterType<IExternalDataRepository, ExternalDataRepository>();
		    container.RegisterType<IVisaValidationManager, VisaValidationManager>();
		    container.RegisterType<IQueueRegisterRepository, QueueRegisterRepository>();
			container.RegisterType<IQueueInfrastructure, QueueInfrastructure>();
			container.RegisterType<IRoutesInfrastructure, RoutesInfrastructure>();
			container.RegisterType<IOpDataManager, OpDataManager>();
			container.RegisterType<IUserManager, UserManager>();
			container.RegisterType<ILoadPointManager, LoadPointManager>();
			container.RegisterType<IUnloadPointManager, UnloadPointManager>();
			container.RegisterType<ICardManager, CardManager>();
			container.RegisterType<IScaleManager, ScaleManager>();
			
			container.RegisterType<IRfidObidRwManager, RfidObidRwManager>();
			container.RegisterType<IRfidZebraFx9500Manager, RfidZebraFx9500Manager>();
			container.RegisterType<IVkModuleSocket1Manager, VkModuleSocket1Manager>();
			container.RegisterType<IVkModuleSocket2Manager, VkModuleSocket2Manager>();
			container.RegisterType<IScaleMettlerPT6S3Manager, ScaleMettlerPT6S3Manager>();
			container.RegisterType<ILabBrukerManager, LabBrukerManager>();
			container.RegisterType<ILabFossManager, LabFossManager>();
			container.RegisterType<ILabFossManager2, LabFossManager2>();
			container.RegisterType<ILabInfrascanManager, LabInfrascanManager>();
			container.RegisterType<IOneCApiService, OneCApiService>();
			container.RegisterInstance(new OneCApiClient(GlobalConfigurationManager.OneCApiHost));

            container.RegisterInstance<IQueueManager>(new QueueManager(container.Resolve<INodeRepository>(),
		        container.Resolve<IConnectManager>(), container.Resolve<IOpDataRepository>(),
		        container.Resolve<IRoutesRepository>(), container.Resolve<ITicketRepository>(),
		        container.Resolve<IQueueSettingsRepository>(), container.Resolve<ITrafficRepository>(),
                container.Resolve<IExternalDataRepository>(),
                container.Resolve<IQueueRegisterRepository>(),
                container.Resolve<INodeManager>(),
                container.Resolve<IRoutesInfrastructure>(),
		        container.Resolve<GravitasDbContext>()
            ));
        }
	}
}