using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.BlackList;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.EmployeeRoles;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.OrganizationUnit;
using Gravitas.DAL.Repository.OwnTransport;
using Gravitas.DAL.Repository.PackingTare;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.PreRegistration;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.Repository.Sms;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.DAL.Repository.Traffic;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.CentralLaboratory;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Display;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.OpVisa;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Infrastructure.Platform.Manager.ReportTool;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.Admin;
using Gravitas.Platform.Web.Manager.BlackList;
using Gravitas.Platform.Web.Manager.CollisionManager;
using Gravitas.Platform.Web.Manager.Device;
using Gravitas.Platform.Web.Manager.Employee;
using Gravitas.Platform.Web.Manager.MixedFeedManage;
using Gravitas.Platform.Web.Manager.OpData;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Report;
using Gravitas.Platform.Web.Manager.Routes;
using Gravitas.Platform.Web.Manager.Test;
using Gravitas.Platform.Web.Manager.Ticket;
using Gravitas.Platform.Web.Manager.TicketContainer;
using Gravitas.Platform.Web.Manager.Workstation;
using Gravitas.Platform.Web.Manager.User;
using Unity;
using OpRoutineWebManager = Gravitas.Platform.Web.Manager.OpRoutine.OpRoutineWebManager;

namespace Gravitas.Platform.Web.DepedencyInjection
{
	public class WebTypeResolver : ITypeResolver
	{
		public void RegisterType(IUnityContainer container) {
			container.RegisterType<GravitasDbContext, GravitasDbContext>();
			container.RegisterSingleton<ISettings, Settings>();

			container.RegisterType<ICardRepository, CardRepository>();
			container.RegisterType<IOrganizationUnitRepository, OrganizationUnitRepository>();
			container.RegisterType<IOpDataRepository, OpDataRepository>();
			container.RegisterType<INodeRepository, NodeRepository>();
			container.RegisterType<IExternalDataRepository, ExternalDataRepository>();
			container.RegisterType<ITicketRepository, TicketRepository>();
			container.RegisterType<IDeviceRepository, DeviceRepository>();
		    container.RegisterType<IQueueSettingsRepository, QueueSettingsRepository>();
		    container.RegisterType<ITrafficRepository, TrafficRepository>();
		    container.RegisterType<IBlackListRepository, BlackListRepository>();	    
            container.RegisterType<ISmsTemplatesRepository, SmsTemplatesRepository>();
		    container.RegisterType<IEmployeeRolesRepository, EmployeeRolesRepository>();
		    container.RegisterType<IEmployeeWebManager, EmployeeWebManager>();
            container.RegisterType<IAdminWebManager, AdminWebManager>();
            container.RegisterType<IOpDataManager, OpDataManager>();
			container.RegisterType<ICameraManager, CameraManager>();
		    container.RegisterType<IRoutesManager, RoutesManager>();
            container.RegisterInstance<IMessageClient>( 
				new MessageClient (GlobalConfigurationManager.EmailHost, GlobalConfigurationManager.RootEmail,
					GlobalConfigurationManager.RootEmailDestination,
					GlobalConfigurationManager.RootEmailLogin, GlobalConfigurationManager.RootEmailPassword));
            container.RegisterType<IConnectManager, ConnectManager>();
		    container.RegisterType<IQueueDisplay, QueueDisplay>();
            container.RegisterType<ICollisionWebManager, CollisionWebManager>();
            container.RegisterType<IPhonesRepository, PhonesRepository>();
            container
                .RegisterType<IPhoneInformTicketAssignmentRepository, PhoneInformTicketAssignmentRepository>();
			container.RegisterType<IOrganizationUnitWebManager, OrganizationUnitWebManager>();
			container.RegisterType<INodeWebManager, NodeWebManager>();
			container.RegisterType<IOpRoutineManager, OpRoutineManager>();
			container.RegisterType<IOpRoutineWebManager, OpRoutineWebManager>();
			container.RegisterType<IOpDataWebManager, OpDataWebManager>();
			container.RegisterType<IExternalDataWebManager, ExternalDataWebManager>();
			container.RegisterType<ITicketWebManager, TicketWebManager>();
			container.RegisterType<ITicketContainerWebManager, TicketContainerWebManager>();
			container.RegisterType<IDeviceWebManager, DeviceWebManager>();
		    container.RegisterType<IReportWebManager, ReportWebManager>();
			container.RegisterType<IRoutesInfrastructure, RoutesInfrastructure>();
			container.RegisterType<IRoutesRepository, RoutesRepository>();
			container.RegisterType<IReportTool, ReportTool>();
			container.RegisterType<IWorkstationWebManager, WorkstationWebManager>();
			container.RegisterType<IUserWebManager, UserWebManager>();
			container.RegisterType<IRoutesWebManager, RoutesWebManager>();
		    container.RegisterType<IBlackListManager, BlackListManager>();
		    container.RegisterType<IVisaValidationManager, VisaValidationManager>();
		    container.RegisterType<IMixedFeedWebManager, MixedFeedWebManage>();
		    container.RegisterType<IQueueRegisterRepository, QueueRegisterRepository>();
		    container.RegisterType<INodeManager, NodeManager>();
		    container.RegisterType<IQueueInfrastructure, QueueInfrastructure>();
		    container.RegisterType<ICentralLaboratoryManager, CentralLaboratoryManager>();
		    container.RegisterType<IScaleManager, ScaleManager>();
		    container.RegisterType<IPreRegistrationRepository, PreRegistrationRepository>();
		    container.RegisterType<IOwnTransportRepository, OwnTransportRepository>();
		    container.RegisterType<IPackingTareRepository, PackingTareRepository>();

		    container.RegisterInstance<ITestManager>(new TestManager(container.Resolve<INodeRepository>(), container.Resolve<IDeviceRepository>()));

		}
	}
}