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
    public class MixedFeedLoadController : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly IWorkstationWebManager _workstationWebManager;
        private readonly GravitasDbContext _context;

        public MixedFeedLoadController(IOpRoutineWebManager opRoutineWebManager,
            IWorkstationWebManager workstationWebManager, 
            GravitasDbContext context)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _workstationWebManager = workstationWebManager;
            _context = context;
        }

        #region 01_Workstation

        [HttpGet, ChildActionOnly]
        public ActionResult Workstation(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var node = _context.Nodes.First(x => x.Id == nodeId.Value);

            var workstationData = _workstationWebManager.GetWorkstationNodes(node.OrganizationUnitId);
            workstationData.CurrentNodeId = nodeId.Value;
            
            return PartialView("../OpRoutine/MixedFeedLoad/01_Workstation", workstationData);
        }

        [HttpGet]
        public ActionResult SetNodeActive(int? nodeId)
        {
            if (nodeId != null)
            {
                _opRoutineWebManager.MixedFeedLoad_Workstation_SetNodeActive(nodeId.Value);
                var node = _context.Nodes.First(x => x.Id == nodeId.Value);
                if (node.OrganizationUnitId > 0) SignalRInvoke.ReloadHubGroup(node.OrganizationUnitId);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Workstation_Process(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.MixedFeedLoad_Workstation_Process(nodeId.Value);

            return RedirectToAction("Routine", "Node", new {Id = nodeId.Value});
        }
        
        [HttpGet]
        public ActionResult Workstation_Cleanup(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.MixedFeedLoad_Workstation_Cleanup(nodeId.Value);

            return RedirectToAction("Routine", "Node", new {Id = nodeId.Value});
        }

        #endregion

        #region 02_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/MixedFeedLoad/02_Idle", _opRoutineWebManager.MixedFeedLoad_IdleVm(nodeId.Value))
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult IdleWorkstation_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_IdleWorkstation_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_Confirm(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_ConfirmOperation_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult Idle_Cancel(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_ConfirmOperation_Cancel(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult Idle_Reject(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_ConfirmOperation_Reject(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        } 
        
        [HttpGet]
        public ActionResult Idle_ChangeState(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_Idle_ChangeState(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 03_AddOperationVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOperationVisa(int nodeId) => PartialView("../OpRoutine/MixedFeedLoad/03_AddOperationVisa", new MixedFeedLoadVms.AddOperationVisaVm
            {
                NodeId = nodeId
            });

        #endregion
        
        #region 04_Cleanup

        [HttpGet, ChildActionOnly]
        public ActionResult Cleanup(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var vm = new MixedFeedLoadVms.CleanupVm
            {
                NodeId = nodeId.Value
            };
            return PartialView("../OpRoutine/MixedFeedLoad/04_Cleanup", vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cleanup(MixedFeedLoadVms.CleanupVm vm)
        {
            if (ModelState.IsValid && vm.CleanupTime > 0 && vm.CleanupTime < 60)
            {
                _opRoutineWebManager.MixedFeedLoad_Cleanup_Start(vm);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        public ActionResult Cleanup_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_Cleanup_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 05_AddCleanupVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddCleanupVisa(int? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/MixedFeedLoad/05_AddCleanupVisa", new MixedFeedLoadVms.AddCleanupVisaVm
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        #endregion
        
        #region 06_AddChangeStateVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddChangeStateVisa(int? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/MixedFeedLoad/06_AddChangeStateVisa", new MixedFeedLoadVms.AddOperationVisaVm()
                {
                    NodeId = nodeId.Value
                })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        [HttpGet]
        public ActionResult AddChangeStateVisa_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.MixedFeedLoad_AddChangeStateVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}