using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.Repository.Node;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel.OpRoutine.MixedFeedGuide;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class MixedFeedGuideController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public MixedFeedGuideController(IOpRoutineWebManager opRoutineWebManager,
            INodeRepository nodeRepository)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _nodeRepository = nodeRepository;
        }

        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new MixedFeedGuideVms.IdleVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/MixedFeedGuide/01_Idle", routineData);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(int? nodeId, int ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.MixedFeedGuide_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
            return RedirectToAction("Routine", "Node", new {Id = nodeId});
        }
        
        #endregion

        #region 02_BindLoadPoint

        [HttpGet, ChildActionOnly]
        public ActionResult BindLoadPoint(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var routineData = _opRoutineWebManager.MixedFeedGuide_BindLoadPoint_GetVm(nodeId.Value);
            return PartialView("../OpRoutine/MixedFeedGuide/02_BindLoadPoint", routineData);
        }

        [HttpGet]
        public ActionResult BindLoadPoint_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.MixedFeedGuide_BindLoadPoint_Back(nodeId.Value);
            return RedirectToAction("Routine", "Node", new {Id = nodeId.Value});
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult BindLoadPoint_Next(MixedFeedGuideVms.BindDestPointVm vm)
        {
            if (vm.DestNodeId != 0) _opRoutineWebManager.MixedFeedGuide_BindLoadPoint_Next(vm);
            return RedirectToAction("Routine", "Node", new {Id = vm.NodeId});
        }

        #endregion
        
        #region 03_AddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOpVisa(int nodeId)
        {
            var routineData = new MixedFeedGuideVms.AddOpVisaVm {NodeId = nodeId};
            return PartialView("../OpRoutine/MixedFeedGuide/03_AddOpVisa", routineData);
        }

        #endregion
    }
}