using System.Web.Http;

namespace Gravitas.CollisionCoordination
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) => config.MapHttpAttributeRoutes();
    }
}