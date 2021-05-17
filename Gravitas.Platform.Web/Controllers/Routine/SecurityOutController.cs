using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class SecurityOutController : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataRepository _opDataRepository;

        public SecurityOutController(IOpRoutineWebManager opRoutineWebManager, INodeRepository nodeRepository, IOpDataRepository opDataRepository)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _nodeRepository = nodeRepository;
            _opDataRepository = opDataRepository;
        }

        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityOutVms.IdleVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityOut/01_Idle", routineData);
        }

        #endregion
        
        #region 02_CheckOwnTransport

        [HttpGet, ChildActionOnly]
        public ActionResult CheckOwnTransport(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityOutVms.CheckOwnTransport() {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityOut/02_CheckOwnTransport", routineData);
        } 
        
        [HttpGet, ChildActionOnly]
        public ActionResult CheckOwnTransport_Next(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SecurityOut_CheckOwnTransport_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet, ChildActionOnly]
        public ActionResult CheckOwnTransport_Reject(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SecurityOut_CheckOwnTransport_Reject(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 03_ShowOperationsList

        [HttpGet, ChildActionOnly]
        public ActionResult ShowOperationsList(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.SecurityOut_ShowOperationsList_GetData(nodeId.Value);

            return PartialView("../OpRoutine/SecurityOut/03_ShowOperationsList", routineData);
        }

        [HttpGet]
        public ActionResult ShowOperationsList_Confirm(long? nodeId, bool isConfirmed)
        {
            if (nodeId != null) _opRoutineWebManager.SecurityOut_ShowOperationsList_Confirm(nodeId.Value, isConfirmed);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 04_EditStampList

        [HttpGet, ChildActionOnly]
        public ActionResult EditStampList(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId.Value);

            var routineData = new SecurityOutVms.EditStampListVm
            {
                NodeId = nodeId.Value,
                IsTechRoute = singleWindowOpData.SupplyCode == Dom.SingleWindowOpData.TechnologicalSupplyCode
            };
            return PartialView("../OpRoutine/SecurityOut/04_EditStampList", routineData);
        }

        [HttpGet]
        public ActionResult EditStampList_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.SecurityOut_EditStampList_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditStampList(SecurityOutVms.EditStampListVm vm)
        {
            _opRoutineWebManager.SecurityOut_EditStampList(vm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 05_AddRouteControlVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddRouteControlVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId.Value);

            var routineData = new SecurityOutVms.AddRouteControlVisaVm
            {
                NodeId = nodeId.Value,
                IsTechRoute = singleWindowOpData.SupplyCode == Dom.SingleWindowOpData.TechnologicalSupplyCode
            };
            return PartialView("../OpRoutine/SecurityOut/05_AddRouteControlVisa", routineData);
        }

        #endregion

        #region 06_AddTransportInspectionVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddTransportInspectionVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId.Value);

            var routineData = new SecurityOutVms.AddTransportInspectionVisaVm
            {
                NodeId = nodeId.Value,
                IsTechRoute = singleWindowOpData.SupplyCode == Dom.SingleWindowOpData.TechnologicalSupplyCode
            };
            return PartialView("../OpRoutine/SecurityOut/06_AddTransportInspectionVisa", routineData);
        }

        #endregion

        #region 07_OpenBarrier

        [HttpGet, ChildActionOnly]
        public ActionResult OpenBarrier(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityOutVms.OpenBarrierVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityOut/07_OpenBarrier", routineData);
        }

        #endregion

        #region 08_GetCamSnapshot

        [HttpGet, ChildActionOnly]
        public ActionResult GetCamSnapshot(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityOutVms.GetCamSnapshotVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityOut/08_GetCamSnapshot", routineData);
        }

        #endregion

        #region 10_NonStandartPass

        [HttpGet, ChildActionOnly]
        public ActionResult NonStandardPass(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityOutVms.NonStandartPassVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityOut/10_NonStandartPass", routineData);
        }

        #endregion
        
    }
}