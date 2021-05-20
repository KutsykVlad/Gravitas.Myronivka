using System.Net;
using System.Web.Mvc;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide2;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadPointGuide2Controller : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public UnloadPointGuide2Controller(IOpRoutineWebManager opRoutineWebManager)
        {
            _opRoutineWebManager = opRoutineWebManager;
        }

        #region 01_Idle

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new UnloadPointGuide2Vms.IdleVm
            {
                NodeId = nodeId.Value
            };
            return PartialView("../OpRoutine/UnloadPointGuide2/01_Idle", routineData);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(int? nodeId, int ticketContainerId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointGuide2_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 02_BindUnloadPoint

        [HttpGet]
        [ChildActionOnly]
        public ActionResult BindUnloadPoint(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.UnloadPointGuide2_BindUnloadPoint_GetVm(nodeId.Value);
            return PartialView("../OpRoutine/UnloadPointGuide2/02_BindUnloadPoint", routineData);
        }
        
        
        [HttpGet]
        public ActionResult BindUnloadPoint_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointGuide2_BindUnloadPoint_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BindUnloadPoint_Next(UnloadPointGuide2Vms.BindUnloadPointVm vm)
        {
            _opRoutineWebManager.UnloadPointGuide2_BindUnloadPoint_Next(vm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 03_AddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddOpVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new UnloadPointGuide2Vms.AddOpVisaVm
            {
                NodeId = nodeId.Value
            };
            return PartialView("../OpRoutine/UnloadPointGuide2/03_AddOpVisa", routineData);
        }

        #endregion
    }
}