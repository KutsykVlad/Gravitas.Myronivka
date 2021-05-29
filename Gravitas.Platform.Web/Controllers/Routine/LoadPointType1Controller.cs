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
    public class LoadPointType1Controller : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly IWorkstationWebManager _workstationWebManager;
        private readonly GravitasDbContext _context;

        public LoadPointType1Controller(IOpRoutineWebManager opRoutineWebManager,
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
            
            var workstationData = _workstationWebManager.GetWorkstationNodes(node.OrganizationUnitId ?? 0);
            workstationData.CurrentNodeId = nodeId.Value;
            return PartialView("../OpRoutine/LoadPointType1/Workstation", workstationData);
        }

        [HttpGet]
        public ActionResult SetNodeActive(int? nodeId)
        {
            if (nodeId != null)
            {
                _opRoutineWebManager.LoadPointType1_Workstation_SetNodeActive(nodeId.Value);
                var node = _context.Nodes.First(x => x.Id == nodeId.Value);
                if (node.OrganizationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(node.OrganizationUnitId.Value);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Workstation_Process(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.LoadPointType1_Workstation_Process(nodeId.Value);

            return RedirectToAction("Routine", "Node", new {Id = nodeId.Value});
        }

        #endregion

        #region Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LoadPointType1/Idle", _opRoutineWebManager.LoadPointType1_IdleVm(nodeId.Value))
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult IdleWorkstation_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_IdleWorkstation_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_Confirm(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_ConfirmOperation_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult Idle_Cancel(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_ConfirmOperation_Cancel(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Idle_Reject(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_ConfirmOperation_Reject(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        } 
        
        public ActionResult Idle_ChangeState(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_Idle_ChangeState(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult Idle_GetTareValue(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_Idle_GetTareValue(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region GetTareValue

        [HttpGet, ChildActionOnly]
        public ActionResult GetTareValue(int? nodeId)
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
        public ActionResult AddChangeStateVisa(int? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LoadPointType1/AddChangeStateVisa", new LoadPointType1Vms.AddOperationVisaVm()
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        [HttpGet]
        public ActionResult AddChangeStateVisa_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LoadPointType1_AddChangeStateVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}