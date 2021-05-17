using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Workstation;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadPointType2Controller : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IWorkstationWebManager _workstationWebManager;

        public UnloadPointType2Controller(IOpRoutineWebManager opRoutineWebManager,
            IWorkstationWebManager workstationWebManager,
            INodeRepository nodeRepository)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _workstationWebManager = workstationWebManager;
            _nodeRepository = nodeRepository;
        }



        #region Workstation

        [HttpGet, ChildActionOnly]
        public ActionResult Workstation(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var node = _nodeRepository.GetEntity<Node, long>(nodeId.Value);

            var workstationData =
                _workstationWebManager.GetWorkstationNodes(node.OrganisationUnitId ?? 0);
            workstationData.CurrentNodeId = nodeId.Value;
            return PartialView("../OpRoutine/UnloadPointType2/Workstation", workstationData);
        }

        [HttpGet]
        public ActionResult SetNodeActive(long? nodeId)
        {
            if (nodeId != null)
            {
                _opRoutineWebManager.UnloadPointType2_Workstation_SetNodeActive(nodeId.Value);
                var node = _nodeRepository.GetEntity<Node, long>(nodeId.Value);
                if (node.OrganisationUnitId.HasValue)
                     SignalRInvoke.ReloadHubGroup(node.OrganisationUnitId.Value);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Workstation_Process(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.UnloadPointType2_Workstation_Process(nodeId.Value);

            return RedirectToAction("Routine", "Node", new { Id = nodeId.Value });
        }

        #endregion


        #region Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(long? nodeId) => nodeId.HasValue
            ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/Idle",
               _opRoutineWebManager.UnloadPointType2_IdleVm(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult IdleWorkstation_Back(long? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_IdleWorkstation_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }



        [HttpGet]
        public ActionResult Idle_ChangeState(long? nodeId)
        {
            if (nodeId != null)
                 _opRoutineWebManager.UnloadPointType2_Idle_ChangeState(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region GetTareValue

        [HttpGet, ChildActionOnly]
        public ActionResult SelectAcceptancePoint(long? nodeId) => nodeId.HasValue
            ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/SelectAcceptancePoints",
                new UnloadPointType2Vms.SelectAcceptancePoint() { NodeId = nodeId.Value })
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult SelectAcceptancePoint_Confirm(long? nodeId, string acceptancePointCode)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_ConfirmOperation_Next(nodeId.Value, acceptancePointCode);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region AddOperationVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOperationVisa(long? nodeId) => nodeId.HasValue
            ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/AddOperationVisa",
                new UnloadPointType2Vms.AddOperationVisaVm { NodeId = nodeId.Value })
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult AddOperationVisa_Back(long? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_AddOperationVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region AddChangeStateVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddChangeStateVisa(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/AddChangeStateVisa", new UnloadPointType2Vms.AddOperationVisaVm()
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult AddChangeStateVisa_Back(long? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_AddChangeStateVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}