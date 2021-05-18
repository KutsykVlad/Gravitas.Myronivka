using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class SecurityInController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataManager _opDataManager;
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public SecurityInController(IOpRoutineWebManager opRoutineWebManager,
            INodeRepository nodeRepository,
            IOpDataManager opDataManager)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _nodeRepository = nodeRepository;
            _opDataManager = opDataManager;
        }

        #region CancelOperation

        [HttpGet]
        public ActionResult CancelOperation(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.SecurityIn_Entry_Cancelation(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 01_Idle

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Idle(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityInVms.IdleVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityIn/01_Idle", routineData);
        }

        #endregion

        #region 03_BindLongRangeRfid

        [HttpGet]
        [ChildActionOnly]
        public ActionResult BindLongRangeRfid(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var routineData = new SecurityInVms.BindLongRangeRfidVm
            {
                NodeId = nodeId.Value
            };

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId != null)
                routineData.TruckBaseInfo = _opDataManager.GetBasicTicketData(nodeDto.Context.TicketId.Value);

            return PartialView("../OpRoutine/SecurityIn/03_BindLongRangeRfid", routineData);
        }

        #endregion

        #region 04_AddOperationVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AddOperationVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityInVms.AddOperationVisaVm
            {
                NodeId = nodeId.Value
            };
            
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            var nodeContext = _nodeRepository.GetNodeContext(nodeId.Value);
            if (nodeContext.OpProcessData.HasValue)
            {
                var own = _nodeRepository.GetEntity<OwnTransport, long>(nodeContext.OpProcessData.Value);
                routineData.OwnTruck = own.TruckNo;
                routineData.OwnTrailer = own.TrailerNo;
            }
            if (nodeDto?.Context?.TicketId != null)
                routineData.TruckBaseInfo = _opDataManager.GetBasicTicketData(nodeDto.Context.TicketId.Value);

            return PartialView("../OpRoutine/SecurityIn/04_AddOperationVisa", routineData);
        }

        #endregion

        #region 05_OpenBarrier

        [HttpGet]
        [ChildActionOnly]
        public ActionResult OpenBarrier(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityInVms.OpenBarrierVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityIn/05_OpenBarrier", routineData);
        }

        #endregion

        #region 06_GetCamSnapshot

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetCamSnapshot(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityInVms.GetCamSnapshotVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityIn/06_GetCamSnapshot", routineData);
        }

        #endregion

        #region 10_NonStandartPass

        [HttpGet]
        [ChildActionOnly]
        public ActionResult NonStandardPass(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityInVms.NonStandartPassVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityIn/10_NonStandartPass", routineData);
        }

        #endregion

        #region 02_CheckOwnTransport

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CheckOwnTransport(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SecurityInVms.CheckOwnTransport {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SecurityIn/02_CheckOwnTransport", routineData);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CheckOwnTransport_Next(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SecurityIn_CheckOwnTransport_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CheckOwnTransport_Reject(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SecurityIn_CheckOwnTransport_Reject(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}