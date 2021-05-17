using Gravitas.Core.DependencyInjection;
using Gravitas.Infrastructure.Platform.DependencyInjection;

namespace Gravitas.Core
{
	public static class Bootstrapper {

		public static void Initialize() {
			DependencyResolverConfig.RegisterType(new CoreTypeResolver());
		}
	}
}