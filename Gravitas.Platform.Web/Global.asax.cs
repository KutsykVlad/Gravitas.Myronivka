using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Gravitas.Platform.Web.Attribute;
using NLog;

namespace Gravitas.Platform.Web
{
    public class MvcApplication : HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bootstrapper.Bootstrapper.Initialize();
            FilterProviders.Providers.Add(new PerformanceTestFilterProvider());
        }

//        protected void Application_EndRequest()
//        {
//            if (Context.Response.StatusCode == 404)
//            {
//                Logger.Error($"404 !!! Page = {Context.Request.Url}");
//            }
//            
//            if (Context.Response.StatusCode == 500)
//            {
//                Logger.Error($"500 !!! Page = {Context.Request.Url}");
//            }
//            
//            if (Context.Response.StatusCode == 400)
//            {
//                Logger.Error($"400 !!! Page = {Context.Request.Url}");
//            }
//        }
    }
}