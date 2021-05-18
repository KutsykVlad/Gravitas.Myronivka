using System.Net;
using System.Web;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class LaboratoryInController : Controller
    {
        private readonly IExternalDataWebManager _externalDataManager;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly ITicketWebManager _ticketWebManager;
        private readonly INodeRepository _nodeRepository;

        public LaboratoryInController(IOpRoutineWebManager opRoutineWebManager,
            IExternalDataWebManager externalDataManager, 
            ITicketWebManager ticketWebManager, 
            INodeRepository nodeRepository)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _externalDataManager = externalDataManager;
            _ticketWebManager = ticketWebManager;
            _nodeRepository = nodeRepository;
        }

      
        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/01_Idle",
                    new LaboratoryInVms.IdleVm
                    {
                        NodeId = nodeId.Value,
                        IsBusy = _nodeRepository.GetNodeContext(nodeId.Value)?.OpProcessData != null
                    })
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(long? nodeId, long ticketContainerId)
        {
            if (nodeId != null)
                _opRoutineWebManager.LaboratoryIn_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
            
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_СollectSample(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_Idle_СollectSample(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_EditAnalysisResult(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_Idle_EditAnalysisResult(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_PrintAnalysisResult(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_Idle_PrintAnalysisResult(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult Idle_PrintCollisionInit(long? nodeId, int ticketId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_Idle_PrintCollisionInit(nodeId.Value, ticketId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 02_SampleReadTruckRfid

        [HttpGet, ChildActionOnly]
        public ActionResult SampleReadTruckRfid(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/02_SampleReadTruckRfid",
                new LaboratoryInVms.SampleReadTruckRfidVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult SampleReadTruckRfid_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_SampleReadTruckRfid_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 03_SampleBindTray

        [HttpGet, ChildActionOnly]
        public ActionResult SampleBindTray(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/03_SampleBindTray",
                new LaboratoryInVms.SampleBindTrayVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult SampleBindTray_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_SampleBindTray_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 04_SampleBindAnalysisTray

        [HttpGet, ChildActionOnly]
        public ActionResult SampleBindAnalysisTray(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/04_SampleBindAnalysisTray",
                _opRoutineWebManager.LaboratoryIn_SampleBindAnalysisTray_GetVmData(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult SampleBindAnalysisTray_Next(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_SampleBindAnalysisTray_Next(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SampleBindAnalysisTray(LaboratoryInVms.SampleBindAnalysisTrayVm vm)
        {
            _opRoutineWebManager.LaboratoryIn_SampleBindAnalysisTray(vm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 05_SampleAddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult SampleAddOpVisa(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/05_SampleAddOpVisa",
                new LaboratoryInVms.SampleAddOpVisaVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #endregion

        #region 11_ResultReadTrayRfid

        [HttpGet, ChildActionOnly]
        public ActionResult ResultReadTrayRfid(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/11_ResultReadTrayRfid",
                new LaboratoryInVms.ResultReadTrayRfidVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult ResultReadTrayRfid_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_ResultReadTrayRfid_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 12_ResultEditAnalysis

        [HttpGet, ChildActionOnly]
        public ActionResult ResultEditAnalysis(long? nodeId)
        {
            if (nodeId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var vm = _opRoutineWebManager.LaboratoryIn_ResultEditAnalysis_GetVm(nodeId.Value);

            ViewBag.LabImpurityСlassifierItems = _externalDataManager.GetLabImpurityСlassifierItemsVm();
            ViewBag.LabHumidityСlassifierItems = _externalDataManager.GetLabHumidityСlassifierItemsVm();
            ViewBag.LabInfectionedСlassifierItems = _externalDataManager.GetLabInfectionedСlassifierItemsVm();

            return PartialView("../OpRoutine/LabolatoryIn/12_ResultEditAnalysis", vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResultEditAnalysis_Save(LaboratoryInVms.LabFacelessOpDataComponentDetailVm vm)
        {
            _opRoutineWebManager.LaboratoryIn_ResultEditAnalysis_Save(vm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResultEditAnalysis_SaveFromDevice(DeviceStateVms.LabAnalyserStateDialogVm vm)
        {
            _opRoutineWebManager.LaboratoryIn_ResultEditAnalysis_SaveFromDevice(vm);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 13_ResultAddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult ResultAddOpVisa(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/13_ResultAddOpVisa",
                new LaboratoryInVms.ResultAddOpVisaVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #endregion

        #region 21_PrintReadTrayRfid

        [HttpGet, ChildActionOnly]
        public ActionResult PrintReadTrayRfid(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/21_PrintReadTrayRfid",
                new LaboratoryInVms.PrintReadTrayRfidVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult PrintReadTrayRfid_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintReadTrayRfid_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 22_PrintAnalysisResult

        [HttpGet, ChildActionOnly]
        public ActionResult PrintAnalysisResult(long? nodeId)
        {
            if (nodeId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData =
                _opRoutineWebManager.LaboratoryIn_PrintAnalysisResults_GetVmData(nodeId.Value);

            ViewBag.LabImpurityСlassifierItems = _externalDataManager.GetLabImpurityСlassifierItemsVm();
            ViewBag.LabHumidityСlassifierItems = _externalDataManager.GetLabHumidityСlassifierItemsVm();
            ViewBag.LabInfectionedСlassifierItems = _externalDataManager.GetLabInfectionedСlassifierItemsVm();
            ViewBag.LabEffectiveСlassifierItems = _externalDataManager.GetLabEffectiveСlassifierItemsVm();

            return PartialView("../OpRoutine/LabolatoryIn/22_PrintAnalysisResult", routineData);
        }

        [HttpGet]
        public ActionResult PrintAnalysisResult_Back(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintAnalysisResult_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PrintAnalysisResult_Save(LaboratoryInVms.LabFacelessOpDataDetailVm vm)
        {
            if (ModelState.IsValid)
            {
                _opRoutineWebManager.LaboratoryIn_PrintAnalysisResult_Save(vm);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 23_PrintAnalysisAddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult PrintAnalysisAddOpVisa(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/23_PrintAnalysisAddOpVisa",
                new LaboratoryInVms.ResultAddOpVisaVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #endregion

        #region 24_PrintDataDisclose

        [HttpGet, ChildActionOnly]
        public ActionResult PrintDataDisclose(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/24_PrintDataDisclose",
                _opRoutineWebManager.LaboratoryIn_PrintDataDisclose_GetVm(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult PrintDataDisclose_Confirm(long? nodeId, bool isConfirmed)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintDataDisclose_Confirm(nodeId.Value, isConfirmed);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult PrintDataDisclose_Cancel(long? nodeId, string indexRefundReason)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintCollisionManage_SetReturnRoute(nodeId.Value, indexRefundReason);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        } 
        
        [HttpGet]
        public ActionResult PrintDataDisclose_Reload(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintCollisionManage_SetReloadRoute(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PrintDataDisclose_SaveFile(long nodeId, HttpPostedFileBase laboratoryFile)
        {
            _ticketWebManager.UploadFile(nodeId, laboratoryFile, Dom.TicketFile.Type.LabCertificate);
            SignalRInvoke.ReloadHubGroup(nodeId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 25_PrintCollisionInit

        [HttpGet, ChildActionOnly]
        public ActionResult PrintCollisionInit(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/25_PrintCollisionInit",
                _opRoutineWebManager.LaboratoryIn_GetCollisionInitVm(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        [HttpGet]
        public ActionResult PrintCollisionInit_Return(long? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintCollisionInit_Return(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult PrintCollisionInit_Process(LaboratoryInVms.PrintCollisionInitVm vm)
        {
            SignalRInvoke.StartSpinner(vm.NodeId);
            _opRoutineWebManager.LaboratoryIn_SendToCollision(vm);
            SignalRInvoke.StopSpinner(vm.NodeId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 26_PrintCollisionManage

        [HttpGet, ChildActionOnly]
        public ActionResult PrintCollisionManage(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/26_PrintCollisionManage",
                _opRoutineWebManager.GetLaboratoryIn_PrintCollisionManageVm(nodeId.Value))
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);


        [HttpGet]
        public ActionResult PrintCollisionManage_ReturnToCollectSamples(long? nodeId)
        {
            if (nodeId != null)
                _opRoutineWebManager.LaboratoryIn_PrintCollisionManage_ReturnToCollectSamples(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult PrintCollisionManage_SetRouteToExit(long? nodeId, string indexRefundReason)
        {
            if (nodeId != null) _opRoutineWebManager.LaboratoryIn_PrintCollisionManage_SetReturnRoute(nodeId.Value, indexRefundReason);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 27_PrintAddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult PrintAddOpVisa(long? nodeId) => nodeId.HasValue
            ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/27_PrintAddOpVisa",
                new LaboratoryInVms.PrintAddOpVisaVm {NodeId = nodeId.Value})
            : new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        #endregion
        
        #region 28_PrintLaboratoryProtocol

        [HttpGet, ChildActionOnly]
        public ActionResult PrintLaboratoryProtocol(long? nodeId)
        {
            return nodeId.HasValue
                ? (ActionResult) PartialView("../OpRoutine/LabolatoryIn/28_PrintLaboratoryProtocol",
                    _opRoutineWebManager.PrintLaboratoryProtocol_GetVm(nodeId.Value))
                : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult PrintLaboratoryProtocol_Next(long nodeId)
        {
            _opRoutineWebManager.PrintLaboratoryProtocol_Next(nodeId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

       
    }
}