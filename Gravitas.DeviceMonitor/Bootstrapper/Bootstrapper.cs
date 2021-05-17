using Gravitas.DeviceMonitor.DependencyInjection;
using Gravitas.Infrastructure.Platform.DependencyInjection;

namespace Gravitas.DeviceMonitor.Bootstrapper 
{ 
	public static class Bootstrapper 
	{
		public static void Initialize() 
		{
			DependencyResolverConfig.RegisterType(new DeviceMonitorTypeResolver());
		}
	}
}