﻿using Gravitas.DAL;
using Gravitas.DeviceMonitor.Manager;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Platform.Web.Manager.Report;
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
				new MessageClient (GlobalConfigurationManager.EmailHost, GlobalConfigurationManager.RootEmail,
					GlobalConfigurationManager.RootEmailDestination,
					GlobalConfigurationManager.RootEmailLogin, GlobalConfigurationManager.RootEmailPassword));
			container.RegisterType<INodeRepository, NodeRepository>();
			container.RegisterType<IConnectManager, ConnectManager>();
		    
		    container.RegisterType<IDeviceMonitorManager, DeviceMonitorManager>();
		}
	}
}