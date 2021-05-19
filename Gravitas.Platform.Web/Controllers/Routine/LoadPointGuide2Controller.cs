using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.Repository.Node;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel.OpRoutine.LoadPointGuide2;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class LoadPointGuide2Controller : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public LoadPointGuide2Controller(IOpRoutineWebManager opRoutineWebManager,
            INodeRepository nodeRepository)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _nodeRepository = nodeRepository;
        }

        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new LoadPointGuide2Vms.IdleVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/LoadPointGuide2/01_Idle", routineData);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(long? nodeId, long ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.LoadPointGuide2_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
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

            var routineData = _opRoutineWebManager.LoadPointGuide2_BindLoadPoint_GetVm(nodeId.Value);
            return PartialView("../OpRoutine/LoadPointGuide2/02_BindLoadPoint", routineData);
        }

        [HttpGet]
        public ActionResult BindLoadPoint_Back(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.LoadPointGuide2_BindLoadPoint_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult BindLoadPoint_Next(LoadPointGuide2Vms.BindDestPointVm vm)
        {
            if (vm.DestNodeId != 0) _opRoutineWebManager.LoadPointGuide2_BindLoadPoint_Next(vm);
            return RedirectToAction("Routine", "Node", new {Id = vm.NodeId});
        }

        #endregion
        
        #region 03_AddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOpVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new LoadPointGuide2Vms.AddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/LoadPointGuide2/03_AddOpVisa", routineData);
        } 
        
        // [HttpGet]
        // public ActionResult AddOpVisa_Back(long? nodeId)
        // {
        //     if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //
        //     _opRoutineWebManager.AddOpVisa_Back(nodeId.Value);
        //     return new HttpStatusCodeResult(HttpStatusCode.OK);
        // }

        #endregion
    }
}