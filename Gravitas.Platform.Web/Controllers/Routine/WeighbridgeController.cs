using System.Net;
using System.Web.Mvc;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;
using NLog;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class WeighbridgeController : Controller
    {
        private readonly IOpRoutineWebManager _opRoutineWebManager;

        public WeighbridgeController(IOpRoutineWebManager opRoutineWebManager)
        {
            _opRoutineWebManager = opRoutineWebManager;
        }

        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/01_Idle", new WeightbridgeVms.IdleVm {NodeId = nodeId});
        }

        #endregion

        #region 02_GetScaleZero

        [HttpGet, ChildActionOnly]
        public ActionResult GetScaleZero(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/02_GetScaleZero", new WeightbridgeVms.GetScaleZeroVm
            {
                NodeId = nodeId
            });
        }

        #endregion

        #region 03_OpenBarrierIn

        [HttpGet, ChildActionOnly]
        public ActionResult OpenBarrierIn(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/03_OpenBarrierIn", new WeightbridgeVms.OpenBarrierInVm
            {
                NodeId = nodeId
            });
        }

        #endregion

        #region 04_CheckScaleNotEmpty

        [HttpGet, ChildActionOnly]
        public ActionResult CheckScaleNotEmpty(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/04_CheckScaleNotEmpty", new WeightbridgeVms.CheckScaleNotEmptyVm {NodeId = nodeId});
        }

        #endregion

        #region 05_GetTicketCard

        [HttpGet, ChildActionOnly]
        public ActionResult GetTicketCard(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/05_GetTicketCard", new WeightbridgeVms.GetTicketCardVm {NodeId = nodeId});
        }

        #endregion

        #region 06_DriverTrailerEnableCheck

        [HttpGet, ChildActionOnly]
        public ActionResult DriverTrailerEnableCheck(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return PartialView("../OpRoutine/Weightbridge/06_DriverTrailerEnableCheck", new WeightbridgeVms.DriverTrailerEnableCheckVm {NodeId = nodeId.Value});
        }

        [HttpGet]
        public ActionResult DriverTrailerEnable_Accept(int? nodeId, bool isTrailerAccepted)
        {
            if (nodeId != null) _opRoutineWebManager.Weighbridge_DriverTrailerAccepted(nodeId.Value, isTrailerAccepted);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 07_GuardianCardPrompt

        [HttpGet, ChildActionOnly]
        public ActionResult GuardianCardPrompt(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/07_GuardianCardPrompt", new WeightbridgeVms.GuardianCardPromptVm {NodeId = nodeId});
        }

        #endregion

        #region 08_GuardianTruckVerification

        [HttpGet, ChildActionOnly]
        public ActionResult GuardianTruckVerification(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return PartialView("../OpRoutine/Weightbridge/08_GuardianTruckVerification",
                new WeightbridgeVms.GuardianTruckVerificationVm(_opRoutineWebManager.Weighbridge_GetWeightPrompt_GetData(nodeId.Value)));
        }

        [HttpGet]
        public ActionResult GuardianTruckVerification_Accept(int? nodeId, bool isPermissionGranted)
        {
            if (nodeId != null)  _opRoutineWebManager.Weighbridge_GuardianTruckVerification_Process(nodeId.Value, isPermissionGranted);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 09_GuardianTrailerEnableCheck

        [HttpGet, ChildActionOnly]
        public ActionResult GuardianTrailerEnableCheck(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return PartialView("../OpRoutine/Weightbridge/09_GuardianTrailerEnableCheck",
                new WeightbridgeVms.GuardianTrailerEnableCheckVm(_opRoutineWebManager.Weighbridge_GetWeightPrompt_GetData(nodeId.Value)));
        }

        [HttpGet]
        public ActionResult GuardianTrailerEnable_Accept(int? nodeId, bool isTrailerAccepted)
        {
            if (nodeId != null) _opRoutineWebManager.Weighbridge_GuardianTrailerEnable_Process(nodeId.Value, isTrailerAccepted);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 10_TruckWeightPrompt

        [HttpGet, ChildActionOnly]
        public ActionResult TruckWeightPrompt(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var routineData = _opRoutineWebManager.WeighbridgeGetTruckWeightPromptVm(nodeId.Value);

            return PartialView("../OpRoutine/Weightbridge/10_TruckWeightPrompt", routineData);
        }

        [HttpGet]
        public ActionResult TruckWeightPrompt_Accept(int? nodeId, bool isWeightAccepted)
        {
            if (nodeId != null) _opRoutineWebManager.Weighbridge_GetWeightPrompt_Process(nodeId.Value, isWeightAccepted, true);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 11_GetGuardianTruckWeightPermission

        [HttpGet, ChildActionOnly]
        public ActionResult GetGuardianTruckWeightPermission(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var routineData = _opRoutineWebManager.Weighbridge__GetGuardianTruckWeightPermissionVm(nodeId.Value);

            return PartialView("../OpRoutine/Weightbridge/11_GetGuardianTruckWeightPermission", routineData);
        }

        #endregion

        #region 12_GetTruckWeight

        [HttpGet, ChildActionOnly]
        public ActionResult GetTruckWeight(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/12_GetTruckWeight", new WeightbridgeVms.GetTruckWeightVm {NodeId = nodeId});
        }

        #endregion

        #region 13_TrailerWeightPrompt

        [HttpGet, ChildActionOnly]
        public ActionResult TrailerWeightPrompt(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var routineData = _opRoutineWebManager.Weighbridge__GetTrailerWeightPromptVm(nodeId.Value);

            return PartialView("../OpRoutine/Weightbridge/13_TrailerWeightPrompt", routineData);
        }

        [HttpGet]
        public ActionResult TrailerWeightPrompt_Accept(int? nodeId, bool isWeightAccepted)
        {
            if (nodeId != null) _opRoutineWebManager.Weighbridge_GetWeightPrompt_Process(nodeId.Value, isWeightAccepted, false);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 14_GetGuardianTrailerWeightPermission

        [HttpGet, ChildActionOnly]
        public ActionResult GetGuardianTrailerWeightPermission(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var routineData = _opRoutineWebManager.Weighbridge__GetGuardianTrailerWeightPermissionVm(nodeId.Value);

        
            return PartialView("../OpRoutine/Weightbridge/14_GetGuardianTrailerWeightPermission", routineData);
        }

        #endregion

        #region 15_GetTrailerWeight

        [HttpGet, ChildActionOnly]
        public ActionResult GetTrailerWeight(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/15_GetTrailerWeight", new WeightbridgeVms.GetTrailerWeightVm {NodeId = nodeId});
        }

        #endregion

        #region 16_WeightResultsValidation

        [HttpGet, ChildActionOnly]
        public ActionResult WeightResultsValidation(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/16_WeightResultsValidation", new WeightbridgeVms.WeightResultsValidationVm { NodeId = nodeId });
        }

        #endregion

        #region 17_OpenBarrierOut

        [HttpGet, ChildActionOnly]
        public ActionResult OpenBarrierOut(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return PartialView("../OpRoutine/Weightbridge/17_OpenBarrierOut", _opRoutineWebManager.Weighbridge_OpenBarrierOutVm(nodeId.Value));
        }

        #endregion

        #region 18_CheckScaleEmpty

        [HttpGet, ChildActionOnly]
        public ActionResult CheckScaleEmpty(int nodeId)
        {
            return PartialView("../OpRoutine/Weightbridge/18_CheckScaleEmpty", new WeightbridgeVms.CheckScaleEmptyVm {NodeId = nodeId});
        }

        #endregion

        #region ResetWeightbridge

        [HttpGet]
        public ActionResult ResetWeighbridge(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.Weighbridge_ResetWeighbridge(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

    }
}