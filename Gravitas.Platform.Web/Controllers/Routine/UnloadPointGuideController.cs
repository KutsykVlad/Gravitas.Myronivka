using System.Net;
using System.Web.Mvc;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadPointGuideController : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public UnloadPointGuideController(IOpRoutineWebManager opRoutineWebManager)
        {
            _opRoutineWebManager = opRoutineWebManager;
        }

        #region 03_AddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddOpVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new UnloadPointGuideVms.AddOpVisaVm
            {
                NodeId = nodeId.Value
            };
            return PartialView("../OpRoutine/UnloadPointGuide/03_AddOpVisa", routineData);
        }

        #endregion

        #region 01_Idle

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new UnloadPointGuideVms.IdleVm
            {
                NodeId = nodeId.Value
            };
            return PartialView("../OpRoutine/UnloadPointGuide/01_Idle", routineData);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(int? nodeId, int ticketContainerId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointGuide_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 02_BindUnloadPoint

        [HttpGet]
        [ChildActionOnly]
        public ActionResult BindUnloadPoint(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.UnloadPointGuide_BindUnloadPoint_GetVm(nodeId.Value);
            return PartialView("../OpRoutine/UnloadPointGuide/02_BindUnloadPoint", routineData);
        }
        
        
        [HttpGet]
        public ActionResult Idle_AskFromQueue(int? nodeId, int ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.UnloadPointGuide_Idle_AskFromQueue(nodeId.Value, ticketContainerId);
            return RedirectToAction("Routine", "Node", new { Id = nodeId });
        }

        [HttpGet]
        public ActionResult BindUnloadPoint_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointGuide_BindUnloadPoint_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BindUnloadPoint_Next(UnloadPointGuideVms.BindUnloadPointVm vm)
        {
            _opRoutineWebManager.UnloadPointGuide_BindUnloadPoint_Next(vm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 04_EntryAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult EntryAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new UnloadPointGuideVms.AddOpVisaVm
            {
                NodeId = nodeId.Value
            };
            return PartialView("../OpRoutine/UnloadPointGuide/04_EntryAddOpVisa", routineData);
        }
        
        [HttpGet]
        public ActionResult EntryAddOpVisa_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointGuide_Idle_AskFromQueue_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}