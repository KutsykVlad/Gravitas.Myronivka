using System.Web.Mvc;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadCheckPointController : Controller
    {
        #region 01_Idle

        [HttpGet]
        public ActionResult Idle(int nodeId)
        {
            var vm = new UnloadCheckPointVms.IdleVm { NodeId = nodeId };
            return PartialView("../OpRoutine/UnloadCheckPoint/01_Idle", vm);
        }

        #endregion
    }
}