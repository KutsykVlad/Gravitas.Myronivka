using Gravitas.DAL.Migrations;
using Gravitas.DeviceSync.DependencyInjection;
using Gravitas.Infrastructure.Platform.DependencyInjection;

namespace Gravitas.DeviceSync { 

	public static class Bootstrapper {

		public static void Initialize() {
			DependencyResolverConfig.RegisterType(new DeviceSyncTypeResolver());
		}
	}
}