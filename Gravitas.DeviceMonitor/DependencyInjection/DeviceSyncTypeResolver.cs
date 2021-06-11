using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Sms;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.DeviceMonitor.Manager;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.ReportTool;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Unity;

namespace Gravitas.DeviceMonitor.DependencyInjection
{
	public class DeviceMonitorTypeResolver : ITypeResolver
	{
		public void RegisterType(IUnityContainer container) 
		{
			container.RegisterType<GravitasDbContext, GravitasDbContext>();
			container.RegisterSingleton<ISettings, Settings>();

			container.RegisterType<IRoutesRepository, RoutesRepository>();
			container.RegisterType<IPhonesRepository, PhonesRepository>();
			container.RegisterType<ITicketRepository, TicketRepository>();
			container.RegisterType<IOpDataRepository, OpDataRepository>();
			container.RegisterType<IExternalDataRepository, ExternalDataRepository>();
			container.RegisterType<ISmsTemplatesRepository, SmsTemplatesRepository>();
			container.RegisterType<IReportTool, ReportTool>();
			container.RegisterInstance<IMessageClient>( 
				new MessageClient (GlobalConfigurationManager.EmailHost, 
					GlobalConfigurationManager.RootEmailLogin, 
					GlobalConfigurationManager.RootEmailPassword,
					GlobalConfigurationManager.OmniHost,
					GlobalConfigurationManager.OmniLogin,
					GlobalConfigurationManager.OmniPassword));
			container.RegisterType<INodeRepository, NodeRepository>();
			container.RegisterType<IConnectManager, ConnectManager>();
		    
		    container.RegisterType<IDeviceMonitorManager, DeviceMonitorManager>();
		}
	}
}