using Unity;

namespace Gravitas.Infrastructure.Platform.DependencyInjection
{
    public interface ITypeResolver
    {
        void RegisterType(IUnityContainer container);
    }
}