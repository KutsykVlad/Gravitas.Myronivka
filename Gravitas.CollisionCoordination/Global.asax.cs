using System.Web;
using System.Web.Http;
using Gravitas.DAL.Migrations;

namespace Gravitas.CollisionCoordination
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
        }
    }
}