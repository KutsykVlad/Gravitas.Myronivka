using AutoMapper;
using Gravitas.Connect.AutoMapper;
using Gravitas.DeviceSync.DependencyInjection;
using Gravitas.Infrastructure.Platform.DependencyInjection;

namespace Gravitas.Connect { 

	public static class Bootstrapper {

		public static void Initialize() {
			DependencyResolverConfig.RegisterType(new ConnectTypeResolver());

			Mapper.Initialize(cfg => {
				cfg.AddProfile<Api1cProfile>();
			});
			Mapper.AssertConfigurationIsValid();
		}
	}
}