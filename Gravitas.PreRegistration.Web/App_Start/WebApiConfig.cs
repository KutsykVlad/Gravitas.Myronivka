using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Gravitas.PreRegistration.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
            config.Routes.MapHttpRoute(
                name: "ApiById",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^[0-9]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiByName",
                routeTemplate: "api/{controller}/{action}/{name}",
                defaults: null,
                constraints: new { name = @"^[a-z]+$" }
            );


            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "Get" }
            );
            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}