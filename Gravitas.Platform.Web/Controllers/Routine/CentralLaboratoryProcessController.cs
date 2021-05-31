using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Ticket;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class CentralLaboratoryProcessController : Controller
    {
        private readonly ITicketWebManager _ticketWebManager;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly INodeRepository _nodeRepository;
        private readonly GravitasDbContext _context;
        private readonly IOpRoutineManager _opRoutineManager;

        public CentralLaboratoryProcessController(IOpRoutineWebManager opRoutineWebManager
            , ITicketWebManager ticketWebManager, 
            INodeRepository nodeRepository,
            GravitasDbContext context, 
            IOpRoutineManager opRoutineManager)
        {
            _ticketWebManager = ticketWebManager;
            _nodeRepository = nodeRepository;
            _context = context;
            _opRoutineManager = opRoutineManager;
            _opRoutineWebManager = opRoutineWebManager;
        }

        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.CentralLaboratoryProcess_Idle_GetVm(nodeId.Value);
            return PartialView("../OpRoutine/CentralLabolatoryProcess/01_Idle", routineData);
        }

        public ActionResult Idle_SelectTicket(int? nodeId, Guid opDataId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.CentralLaboratoryProcess_Idle_SelectSample(nodeId.Value, opDataId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Idle_AddSample(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.CentralLaboratoryProcess_Idle_AddSample(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 02_AddSample

        [HttpGet, ChildActionOnly]
        public ActionResult AddSample(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new CentralLaboratoryProcess.AddSampleVm() { NodeId = nodeId.Value };
            return PartialView("../OpRoutine/CentralLabolatoryProcess/02_AddSample", routineData);
        }

        [HttpGet]
        public ActionResult AddSample_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.CentralLaboratoryProcess_Idle_AddSample_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 03_AddSampleVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddSampleVisa(int nodeId)
        {
            var routineData = new CentralLaboratoryProcess.AddSampleVisaVm() { NodeId = nodeId };
            return PartialView("../OpRoutine/CentralLabolatoryProcess/03_AddSampleVisa", routineData);
        }


        #endregion
        
        #region 04_PrintLabel

        [HttpGet, ChildActionOnly]
        public ActionResult PrintLabel(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.CentralLaboratory_GetPrintDocumentVm(nodeId.Value);
            return PartialView("../OpRoutine/CentralLabolatoryProcess/04_PrintLabel", routineData);
        }

        public ActionResult PrintLabel_Confirm(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintLabel_Confirm(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 05_PrintDataDisclose

        [HttpGet, ChildActionOnly]
        public ActionResult PrintDataDisclose(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_GetVm(nodeId.Value);
            return PartialView("../OpRoutine/CentralLabolatoryProcess/05_PrintDataDisclose", routineData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PrintDataDisclose_SaveFile(int nodeId, HttpPostedFileBase laboratoryFile)
        {
            _ticketWebManager.UploadFile(nodeId, laboratoryFile, TicketFileType.CentralLabCertificate);
            SignalRInvoke.ReloadHubGroup(nodeId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult PrintDataDisclose_DeleteFile(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_DeleteFile(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult PrintDataDisclose_DownloadFile(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context?.TicketId == null) {
                return null;
            }
		    
            SignalRInvoke.StartSpinner(nodeId);
            var file = _context.TicketFiles.FirstOrDefault(item =>
                item.TicketId == nodeDto.Context.TicketId 
                && item.TypeId == TicketFileType.CentralLabCertificate);
            if (file != null) {
                var fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
                var fileName = file.Name;
                SignalRInvoke.StopSpinner(nodeId);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
			
            _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Немає такого файлу."));
            SignalRInvoke.StopSpinner(nodeId);
            return View();
        }

        [HttpGet]
        public ActionResult PrintDataDisclose_BackToIdle(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult PrintDataDisclose_ReturnToCollectSamples(int? nodeId, string comment)
        {
            if (nodeId != null && !string.IsNullOrEmpty(comment)) _opRoutineWebManager.CentralLaboratoryProcess_PrintCollisionManage_ReturnToCollectSamples(nodeId.Value, comment);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult PrintDataDisclose_Confirm(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_Confirm(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult PrintDataDisclose_MoveWithLoad(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_MoveWithLoad(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult PrintDataDisclose_UnloadToStoreWithLoad(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_UnloadToStoreWithLoad(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult PrintDataDisclose_ToCollisionInit(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDisclose_ToCollisionInit(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 06_PrintCollisionStartVisa

        [HttpGet, ChildActionOnly]
        public ActionResult PrintCollisionStartVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new CentralLaboratoryProcess.AddSampleVisaVm() { NodeId = nodeId.Value };
            return PartialView("../OpRoutine/CentralLabolatoryProcess/06_PrintCollisionStartVisa", routineData);
        }

        
        public ActionResult PrintCollisionStartVisa_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintDataDiscloseVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 07_PrintCollisionInit

        [HttpGet, ChildActionOnly]
        public ActionResult PrintCollisionInit(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.CentralLaboratory_GetCollisionInitVm(nodeId.Value);
            if (routineData == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView("../OpRoutine/CentralLabolatoryProcess/07_PrintCollisionInit", routineData);
        }
        
        [HttpPost]
        public ActionResult PrintCollisionInit_Send(int? nodeId, string comment)
        {
            if (nodeId != null && !string.IsNullOrEmpty(comment)) _opRoutineWebManager.CentralLaboratoryProcess_PrintCollisionInit_Send(nodeId.Value, comment);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult PrintCollisionInit_Process(CentralLaboratoryProcess.PrintCollisionInitVm vm)
        {
            SignalRInvoke.StartSpinner(vm.NodeId);
            _opRoutineWebManager.CentralLaboratoryProcess_SendToCollision(vm);
            SignalRInvoke.StopSpinner(vm.NodeId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult PrintCollisionInit_Return(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintCollisionInit_Return(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 08_PrintCollisionInitVisa

        [HttpGet, ChildActionOnly]
        public ActionResult PrintCollisionInitVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new CentralLaboratoryProcess.AddSampleVisaVm() { NodeId = nodeId.Value };
            return PartialView("../OpRoutine/CentralLabolatoryProcess/08_PrintCollisionInitVisa", routineData);
        }

        public ActionResult PrintCollisionVisa_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintCollisionVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 09_PrintAddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult PrintAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new CentralLaboratoryProcess.PrintAddOpVisaVm() { NodeId = nodeId.Value };
            return PartialView("../OpRoutine/CentralLabolatoryProcess/09_PrintAddOpVisa", routineData);
        }
        
        public ActionResult PrintAddOpVisa_Back(int? nodeId)
        {
            if (nodeId != null) _opRoutineWebManager.CentralLaboratoryProcess_PrintAddOpVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 10_PrintDocument

        [HttpGet, ChildActionOnly]
        public ActionResult PrintDocument(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = _opRoutineWebManager.CentralLaboratory_GetPrintDocumentVm(nodeId.Value);
            return PartialView("../OpRoutine/CentralLabolatoryProcess/10_PrintDocument", routineData);
        }

        [HttpGet]
        public ActionResult PrintDocument_Confirm(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.CentralLaboratoryProcess_PrintDocument_Confirm(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

    }
}