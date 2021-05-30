using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.PackingTare;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Ticket;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class SingleWindowController : Controller
    {
        private readonly IExternalDataWebManager _externalDataManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly ITicketWebManager _ticketWebManager;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly IPackingTareRepository _packingTareRepository;
        private readonly GravitasDbContext _context;
        private readonly IOpDataRepository _opDataRepository;

        public SingleWindowController(
            IOpRoutineWebManager opRoutineWebManager,
            IExternalDataRepository externalDataRepository,
            IExternalDataWebManager externalDataManager,
            ITicketWebManager ticketWebManager,
            INodeRepository nodeRepository, 
            IOpRoutineManager opRoutineManager,
            IPackingTareRepository packingTareRepository, 
            GravitasDbContext context, 
            IOpDataRepository opDataRepository)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _externalDataRepository = externalDataRepository;
            _externalDataManager = externalDataManager;
            _ticketWebManager = ticketWebManager;
            _nodeRepository = nodeRepository;
            _opRoutineManager = opRoutineManager;
            _packingTareRepository = packingTareRepository;
            _context = context;
            _opDataRepository = opDataRepository;
        }

        #region 01_Idle

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.IdleVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/01_Idle", routineData);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(int? nodeId, int ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        #endregion
        
        #region 02_GetTicket

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetTicket(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return PartialView("../OpRoutine/SingleWindow/02_GetTicket", _ticketWebManager.GetTicketVm(nodeId.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTicket(SingleWindowVms.GetTicketVm vmData)
        {
            _opRoutineWebManager.SingleWindow_GetTicket_New(vmData.NodeId, vmData.SupplyBarCode);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult GetTicket_Back(int? nodeId)
        {
            if (nodeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _opRoutineWebManager.SingleWindow_GetTicket_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult GetTicket_Detail(int? nodeId, int? ticketId)
        {
            if (nodeId == null || ticketId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _opRoutineWebManager.SingleWindow_GetTicket_Detail(nodeId.Value, ticketId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult GetTicket_Delete(int? nodeId, int? ticketId)
        {
            if (nodeId == null || ticketId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _opRoutineWebManager.SingleWindow_GetTicket_Delete(nodeId.Value, ticketId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult GetTicket_Close(int? nodeId)
        {
            if (nodeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _opRoutineWebManager.SingleWindow_GetTicket_Close(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 03_ShowTicketMenu

        [HttpGet]
        [ChildActionOnly]
        public ActionResult ShowTicketMenu(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(nodeId.Value);

            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context?.TicketId == null)
                return null;

            var singleWindowOpData = _opDataRepository.GetLastOpData<SingleWindowOpData>(nodeDto.Context.TicketId.Value, null);
            var ticketVm = _ticketWebManager.GetTicketVm(nodeId.Value);

            var routineData = new SingleWindowVms.ShowTicketMenuVm
            {
                NodeId = ticketVm.NodeId,
                TicketContainerId = ticketVm.TicketContainerId,
                TicketId = nodeDto.Context.TicketId.Value,
                TicketStatusId = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value).StatusId,
                SupplyCode = singleWindowOpData?.SupplyCode,
                DeliveryBillId = singleWindowOpData?.DeliveryBillId,
                CardNumber = ticketVm.CardNumber,
                IsThirdPartyCarrier = ticketVm.IsThirdPartyCarrier,
                TransportNo = ticketVm.TransportNo,
                TrailerNo = ticketVm.TrailerNo,
                IsPhoneNumberAvailable = !string.IsNullOrEmpty(singleWindowOpData?.ContactPhoneNo),
                Nomenclature = _externalDataRepository.GetProductDetail(singleWindowOpData?.ProductId)?.ShortName ?? singleWindowOpData?.ProductTitle ?? string.Empty,
                IsEditable = !SingleWindowReadonly.All.Contains((NodeIdValue)ticketVm.NodeId)
            };

            return PartialView("../OpRoutine/SingleWindow/03_ShowTicketMenu", routineData);
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_ShowTicketMenu_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_Route(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_ShowTicketMenu_Route(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_Edit(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_ShowTicketMenu_Edit(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_Commit(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_ShowTicketMenu_Commit(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_PrintDoc(int? nodeId, string printoutTypeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SignalRInvoke.StartSpinner(nodeId.Value);
            var fileBytes = _opRoutineWebManager.SingleWindow_ShowTicketMenu_PrintDoc(nodeId.Value, printoutTypeId);

            if (fileBytes != null)
            {
                var fileName = $@"{printoutTypeId}_{DateTime.Now:yyyy-MM-dd-h24-mm-ss}.pdf";
                SignalRInvoke.StopSpinner(nodeId.Value);
                return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
            }

            _opRoutineManager.UpdateProcessingMessage(nodeId.Value,
                new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Немає такого файлу."));
            SignalRInvoke.StopSpinner(nodeId.Value);
            SignalRInvoke.ReloadHubGroup(nodeId.Value);
            return View();
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_GetLabCertificate(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(nodeId.Value);

            if (nodeDto?.Context?.TicketContainerId == null || nodeDto?.Context?.TicketId == null) return null;

            SignalRInvoke.StartSpinner(nodeId.Value);
            var file = _context.TicketFiles.FirstOrDefault(item =>
                item.TicketId == nodeDto.Context.TicketId
                && (item.TypeId == TicketFileType.LabCertificate || item.TypeId == TicketFileType.CentralLabCertificate));
            if (file != null)
            {
                var fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
                var fileName = file.Name;
                SignalRInvoke.StopSpinner(nodeId.Value);
                return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
            }

            _opRoutineManager.UpdateProcessingMessage(nodeId.Value,
                new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Немає такого файлу."));
            SignalRInvoke.StopSpinner(nodeId.Value);
            SignalRInvoke.ReloadHubGroup(nodeId.Value);
            return View();
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_SendSms(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SignalRInvoke.StartSpinner(nodeId.Value);
            Thread.Sleep(1000);

            _ticketWebManager.SendReplySms(nodeId.Value);

            SignalRInvoke.StopSpinner(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult ShowTicketMenu_Exit(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_ShowTicketMenu_Exit(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeTicketSupplyCode(SingleWindowVms.ShowTicketMenuVm vmData)
        {
            if (!string.IsNullOrWhiteSpace(vmData.SupplyCode))
            {
                SignalRInvoke.StartSpinner(vmData.NodeId);
                _opRoutineWebManager.SingleWindow_GetTicket_Change(vmData.NodeId, vmData.SupplyCode);
                SignalRInvoke.StopSpinner(vmData.NodeId);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DivideTicketCode(SingleWindowVms.ShowTicketMenuVm vmData)
        {
            if (vmData.NewWeightValue >= 500)
            {
                SignalRInvoke.StartSpinner(vmData.NodeId);
                _opRoutineWebManager.SingleWindow_DivideTicket(vmData.NodeId, vmData.NewWeightValue);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 04_ContainerCloseAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult ContainerCloseAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.ContainerCloseAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/04_ContainerCloseAddOpVisa", routineData);
        }

        [HttpGet]
        public ActionResult ContainerCloseAddOpVisa_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_EditAddOpVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 11_EditTicketForm

        [HttpGet]
        [ChildActionOnly]
        public ActionResult EditTicketForm(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null) return null;

            var routineData = new SingleWindowVms.EditTicketFormVm
            {
                NodeId = nodeId.Value,
                SingleWindowOpDataDetailVm = _opRoutineWebManager.SingleWindow_EditTicketForm_GetData(nodeId.Value),
                IsEditable = !SingleWindowReadonly.All.Contains((NodeIdValue)nodeId.Value)
            };

            if (routineData.SingleWindowOpDataDetailVm.SupplyCode == TechRoute.SupplyCode)
            {
                routineData.SingleWindowOpDataDetailVm.IsTechnologicalRoute = true;
            }

            ViewBag.TareList = _packingTareRepository.GetTicketTareList(nodeDto.Context.TicketId.Value);
            ViewBag.GetOrganisationItems = _externalDataManager.GetOrganisationItemsVm();
            ViewBag.SupplyTransportTypeItems = _externalDataManager.GetSupplyTransportTypeItemsVm();

            return PartialView("../OpRoutine/SingleWindow/11_EditTicketForm", routineData);
        }

        [HttpGet]
        public ActionResult EditTicketForm_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_EditTicketForm_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTicketForm_Save(SingleWindowVms.SingleWindowOpDataDetailVm vmData, HttpPostedFileBase laboratoryFile)
        {
            if (ModelState.IsValid)
            {
                if (laboratoryFile != null && vmData.NodeId.HasValue)
                    _ticketWebManager.UploadFile(vmData.NodeId.Value, laboratoryFile, TicketFileType.CustomerLabCertificate);

                _opRoutineWebManager.SingleWindow_EditTicketForm_Save(vmData);
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any()).ToList();
                var err = errors.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage)).ToList();
                _opRoutineManager.UpdateProcessingMessage(vmData.NodeId.Value,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, $"{string.Join(@", ", err)}"));

            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 12_EditGetApiData

        [HttpGet]
        [ChildActionOnly]
        public ActionResult EditGetApiData(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.EditGetApiDataVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/12_EditGetApiData", routineData);
        }

        #endregion

        #region 13_EditAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult EditAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.EditAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/13_EditAddOpVisa", routineData);
        }

        [HttpGet]
        public ActionResult EditAddOpVisa_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_EditAddOpVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 14_EditPostApiData

        [HttpGet]
        [ChildActionOnly]
        public ActionResult EditPostApiData(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.EditPostApiDataVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/14_EditPostApiData", routineData);
        }

        #endregion
        
        #region 15_SupplyChangeAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SupplyChangeAddOpVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.RouteAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/15_SupplyChangeAddOpVisa", routineData);
        }

        #endregion
        
        #region 16_DivideTicketAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DivideTicketAddOpVisa(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.RouteAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/16_DivideTicketAddOpVisa", routineData);
        }
        
        #endregion
        
        #region 17_DeleteTicketAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DeleteTicketAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.RouteAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/17_DeleteTicketAddOpVisa", routineData);
        }
        
        [HttpGet]
        public ActionResult DeleteTicketAddOpVisa_Back(int? nodeId)
        {
            if (nodeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _opRoutineWebManager.SingleWindow_DeleteTicketAddOpVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 21_CloseAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CloseAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.CloseAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/21_CloseAddOpVisa", routineData);
        }

        [HttpGet]
        public ActionResult AddOperationVisa_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _opRoutineWebManager.SingleWindow_AddOperationVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 22_ClosePostApiData

        [HttpGet]
        [ChildActionOnly]
        public ActionResult ClosePostApiData(long? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.ClosePostApiDataVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/22_ClosePostApiData", routineData);
        }

        #endregion

        #region 31_RouteEditData

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RouteEditData(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return PartialView("../OpRoutine/SingleWindow/31_RouteEditData", _opRoutineWebManager.SingleWindow_RouteEditDataVm(nodeId.Value));
        }

        [HttpGet]
        public ActionResult RouteEditData_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_RouteEditData_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RouteEditData_Save(SingleWindowVms.Route vmData)
        {
            if (vmData.SelectedId != -1 && vmData.SelectedId != 0)
            {
                _opRoutineWebManager.SingleWindow_RouteEditData_Save(vmData);
            }
            else
            {
                _opRoutineManager.UpdateProcessingMessage(vmData.NodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Виберіть маршрут"));
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #region 32_RouteAddOpVisa

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RouteAddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new SingleWindowVms.RouteAddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/SingleWindow/32_RouteAddOpVisa", routineData);
        }

        [HttpGet]
        public ActionResult RouteAddOpVisa_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.SingleWindow_RouteAddOpVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}