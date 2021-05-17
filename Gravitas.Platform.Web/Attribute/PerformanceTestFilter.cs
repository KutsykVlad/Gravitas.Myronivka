using System.Diagnostics;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.Attribute
{
    public class PerformanceTestFilter : IActionFilter
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();
            if (_stopWatch.ElapsedMilliseconds > 5000)
                Logger.Info($"Performance measure: Url = {filterContext.HttpContext.Request.Url}, Time = {_stopWatch.Elapsed}");
        }
    }
}