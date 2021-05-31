using System.Web.Mvc;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class LoadCheckPointController : Controller
    {
        #region 01_Idle

        [HttpGet]
        public ActionResult Idle(int nodeId)
        {
            var vm = new LoadCheckPointVms.IdleVm { NodeId = nodeId };
            return PartialView("../OpRoutine/LoadCheckPoint/01_Idle", vm);
        }

        #endregion
    }
}