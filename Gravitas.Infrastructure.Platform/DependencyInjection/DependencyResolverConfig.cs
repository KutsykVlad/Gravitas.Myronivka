using System.Linq;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Resolution;

namespace Gravitas.Infrastructure.Platform.DependencyInjection
{
    public static class DependencyResolverConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void RegisterType(params ITypeResolver[] typeResolvers)
        {
            Container = BindUnityContainer(typeResolvers);
        }

        public static IUnityContainer BindUnityContainer(params ITypeResolver[] typeResolvers)
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            if (typeResolvers != null && typeResolvers.Any())
                foreach (var typeResolver in typeResolvers)
                    typeResolver.RegisterType(container);
            return container;
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(params ResolverOverride[] overrides)
        {
            return Container.Resolve<T>(overrides);
        }
    }
}