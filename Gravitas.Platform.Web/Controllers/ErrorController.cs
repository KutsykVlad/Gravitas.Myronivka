using System.Net;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.Controllers
{
    public class ErrorController : Controller
    {
        private const string Error400ViewUrl = "~/Views/Errors/400.cshtml";
        private const string Error404ViewUrl = "~/Views/Errors/404.cshtml";
        private const string Error500ViewUrl = "~/Views/Errors/500.cshtml";
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public virtual ActionResult NotFound()
        {
            Logger.Warn($"NotFound error");
            return Error(HttpStatusCode.NotFound, Error404ViewUrl);
        }

        public virtual ActionResult ApplicationError()
        {
            Logger.Warn($"InternalServer error");
            return Error(HttpStatusCode.InternalServerError, Error500ViewUrl);
        }

        public virtual ActionResult BadRequest()
        {
            Logger.Warn($"BadRequest error");
            return Error(HttpStatusCode.BadRequest, Error400ViewUrl);
        }

        private ActionResult Error(HttpStatusCode status, string viewName)
        {
            Logger.Info($"User ended up into the error page with status '{status}', view '{viewName}'.");

            return View(viewName);
        }
    }
}