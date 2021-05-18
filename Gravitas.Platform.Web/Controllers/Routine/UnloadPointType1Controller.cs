using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Workstation;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class UnloadPointType1Controller : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly IWorkstationWebManager _workstationWebManager;
        private readonly INodeRepository _nodeRepository;
        
        public UnloadPointType1Controller(IOpRoutineWebManager opRoutineWebManager,
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
            return PartialView("../OpRoutine/UnloadPointType1/Workstation", workstationData);
        }

        [HttpGet]
        public ActionResult SetNodeActive(long? nodeId)
        {
            if (nodeId != null)
            {
                _opRoutineWebManager.UnloadPointType1_Workstation_SetNodeActive(nodeId.Value);
                var node = _nodeRepository.GetEntity<Node, long>(nodeId.Value);
                if (node.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(node.OrganisationUnitId.Value);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Workstation_Process(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.UnloadPointType1_Workstation_Process(nodeId.Value);

            return RedirectToAction("Routine", "Node", new {Id = nodeId.Value});
        }

        #endregion

        #region Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/UnloadPointType1/Idle",
                _opRoutineWebManager.UnloadPointType1_IdleVm(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult IdleWorkstation_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointType1_IdleWorkstation_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_Confirm(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointType1_ConfirmOperation_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult Idle_ChangeState(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointType1_Idle_ChangeState(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult Idle_GetTareValue(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointType1_Idle_GetTareValue(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region GetTareValue

        [HttpGet, ChildActionOnly]
        public ActionResult GetTareValue(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/UnloadPointType1/GetTareValue",
                new UnloadPointType1Vms.GetTareValue() {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #endregion
        
        #region AddOperationVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOperationVisa(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/UnloadPointType1/AddOperationVisa",
                new UnloadPointType1Vms.AddOperationVisaVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #endregion
        
        #region AddChangeStateVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddChangeStateVisa(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/UnloadPointType1/AddChangeStateVisa", new UnloadPointType1Vms.AddOperationVisaVm()
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        [HttpGet]
        public ActionResult AddChangeStateVisa_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.UnloadPointType1_AddChangeStateVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}