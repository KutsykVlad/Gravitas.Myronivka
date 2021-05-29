using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Workstation;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadPointType2Controller : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly IWorkstationWebManager _workstationWebManager;
        private readonly GravitasDbContext _context;

        public UnloadPointType2Controller(IOpRoutineWebManager opRoutineWebManager,
            IWorkstationWebManager workstationWebManager,
            GravitasDbContext context)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _workstationWebManager = workstationWebManager;
            _context = context;
        }

        #region Workstation

        [HttpGet, ChildActionOnly]
        public ActionResult Workstation(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var node = _context.Nodes.First(x => x.Id == nodeId.Value);

            var workstationData =
                _workstationWebManager.GetWorkstationNodes(node.OrganizationUnitId);
            workstationData.CurrentNodeId = nodeId.Value;
            return PartialView("../OpRoutine/UnloadPointType2/Workstation", workstationData);
        }

        [HttpGet]
        public ActionResult SetNodeActive(int? nodeId)
        {
            if (nodeId != null)
            {
                _opRoutineWebManager.UnloadPointType2_Workstation_SetNodeActive(nodeId.Value);
                var node = _context.Nodes.First(x => x.Id == nodeId.Value);
                if (node.OrganizationUnitId > 0)
                     SignalRInvoke.ReloadHubGroup(node.OrganizationUnitId);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Workstation_Process(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.UnloadPointType2_Workstation_Process(nodeId.Value);

            return RedirectToAction("Routine", "Node", new { Id = nodeId.Value });
        }

        #endregion

        #region Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int? nodeId) => nodeId.HasValue
            ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/Idle",
               _opRoutineWebManager.UnloadPointType2_IdleVm(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult IdleWorkstation_Back(int? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_IdleWorkstation_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }



        [HttpGet]
        public ActionResult Idle_ChangeState(int? nodeId)
        {
            if (nodeId != null)
                 _opRoutineWebManager.UnloadPointType2_Idle_ChangeState(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region GetTareValue

        [HttpGet, ChildActionOnly]
        public ActionResult SelectAcceptancePoint(int? nodeId) => nodeId.HasValue
            ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/SelectAcceptancePoints",
                new UnloadPointType2Vms.SelectAcceptancePoint() { NodeId = nodeId.Value })
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult SelectAcceptancePoint_Confirm(int? nodeId, string acceptancePointCode)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_ConfirmOperation_Next(nodeId.Value, acceptancePointCode);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region AddOperationVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOperationVisa(int? nodeId) => nodeId.HasValue
            ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/AddOperationVisa",
                new UnloadPointType2Vms.AddOperationVisaVm { NodeId = nodeId.Value })
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult AddOperationVisa_Back(int? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_AddOperationVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region AddChangeStateVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddChangeStateVisa(int? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult)PartialView("../OpRoutine/UnloadPointType2/AddChangeStateVisa", new UnloadPointType2Vms.AddOperationVisaVm()
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult AddChangeStateVisa_Back(int? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.UnloadPointType2_AddChangeStateVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}