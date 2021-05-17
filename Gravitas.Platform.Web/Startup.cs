using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Gravitas.Platform.Web.Startup))]
namespace Gravitas.Platform.Web {

	public class Startup {

		public void Configuration(IAppBuilder app) {

			app.MapSignalR();
		}
	}
}