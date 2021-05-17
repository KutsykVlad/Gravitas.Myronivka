using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gravitas.PreRegistration.Web.Startup))]
namespace Gravitas.PreRegistration.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
