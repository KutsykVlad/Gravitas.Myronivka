using System.Net;
using System.Web.Mvc;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadCheckPointController : Controller
    {
        #region 01_Idle

        [HttpGet]
        public ActionResult Idle(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var vm = new UnloadCheckPointVms.IdleVm { NodeId = nodeId.Value };
            return PartialView("../OpRoutine/UnloadCheckPoint/01_Idle", vm);
        }

        #endregion
    }
}