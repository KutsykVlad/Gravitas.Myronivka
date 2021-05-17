using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Workstation;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers
{
    public class LoadPointType1Controller : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly IWorkstationWebManager _workstationWebManager;
        private readonly INodeRepository _nodeRepository;

        public LoadPointType1Controller(IOpRoutineWebManager opRoutineWebManager,
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
            
            var workstationData = _workstationWebManager.GetWorkstationNodes(node.OrganisationUnitId ?? 0);
            workstationData.CurrentNodeId = nodeId.Value;
            return PartialView("../OpRoutine/LoadPointType1/Workstation", workstationData);
        }

        [HttpGet]
        public ActionResult SetNodeActive(long? nodeId)
        {
            if (nodeId != null)
            {
                _opRoutineWebManager.LoadPointType1_Workstation_SetNodeActive(nodeId.Value);
                var node = _nodeRepository.GetEntity<Node, long>(nodeId.Value);
                if (node.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(node.OrganisationUnitId.Value);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Workstation_Process(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.LoadPointType1_Workstation_Process(nodeId.Value);

            return RedirectToAction("Routine", "Node", new {Id = nodeId.Value});
        }

        #endregion

        #region Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LoadPointType1/Idle", _opRoutineWebManager.LoadPointType1_IdleVm(nodeId.Value))
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult IdleWorkstation_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_IdleWorkstation_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_Confirm(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_ConfirmOperation_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult Idle_Cancel(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_ConfirmOperation_Cancel(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Idle_Reject(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_ConfirmOperation_Reject(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        } 
        
        public ActionResult Idle_ChangeState(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_Idle_ChangeState(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult Idle_GetTareValue(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_Idle_GetTareValue(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region GetTareValue

        [HttpGet, ChildActionOnly]
        public ActionResult GetTareValue(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LoadPointType1/GetTareValue", new LoadPointType1Vms.GetTareValue
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        #endregion
        
        #region AddOperationVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOperationVisa(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LoadPointType1/AddOperationVisa", new LoadPointType1Vms.AddOperationVisaVm
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        #endregion
        
        #region AddChangeStateVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddChangeStateVisa(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LoadPointType1/AddChangeStateVisa", new LoadPointType1Vms.AddOperationVisaVm()
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        [HttpGet]
        public ActionResult AddChangeStateVisa_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_AddChangeStateVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}