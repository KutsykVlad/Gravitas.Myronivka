using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Gravitas.Platform.Web.AutoMapper;
using Gravitas.Platform.Web.DependencyInjection;
using Unity.Mvc5;

namespace Gravitas.Platform.Web.Bootstrapper
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            DependencyResolverConfig.RegisterType(new WebTypeResolver());

            DependencyResolver.SetResolver(new UnityDependencyResolver(DependencyResolverConfig.Container));

            // GlobalConfiguration.Configuration.DependencyResolver =
            //     new Unity.UnityDependencyResolver(DependencyResolverConfig.Container);

            Mapper.Initialize(cfg => { cfg.AddProfile<DefaultProfile>(); });
            Mapper.AssertConfigurationIsValid();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}