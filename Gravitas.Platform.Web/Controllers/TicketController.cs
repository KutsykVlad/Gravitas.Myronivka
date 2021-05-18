using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketWebManager _ticketWebManager;
        private readonly GravitasDbContext _context;

        public TicketController(INodeRepository nodeRepository, 
            ITicketRepository ticketRepository,
            ITicketWebManager ticketWebManager, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _ticketRepository = ticketRepository;
            _ticketWebManager = ticketWebManager;
            _context = context;
        }

        public ActionResult GetTicketFiles(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var files = _ticketRepository.GetTicketFiles(nodeDto.Context.TicketId.Value)
                .Where(item => item.TypeId == Dom.TicketFile.Type.CustomerLabCertificate)
                .ToList();
            return PartialView("_GetTicketFiles", files);
        }

        public ActionResult DownloadFile(long fileId)
        {
            var file = _context.TicketFiles.First(x => x.Id == fileId);
            var fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
            var fileName = file.Name;
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult History(int? page, string query, DateTime? date)
        {
            var vm = _ticketWebManager.GetHistoryList(page ?? 1, 20, query, date);
            return View("History", vm);
        }

        public ActionResult HistoryGetDetails(string returnUrl, long ticketContainerId, long ticketId)
        {
            var vm = _ticketWebManager.GetHistoryDetails(ticketId, ticketContainerId);
            vm.ReturnUrl = returnUrl;

            return View("HistoryGetDetails", vm);
        }

        [HttpGet]
        public ActionResult PrintDoc(string deliveryBillId, string printoutTypeId, string returnUrl)
        {
            OneCApiClient.GetBillFileDto.Response billFile = null;
            try
            {
                var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
                var request = new OneCApiClient.GetBillFileDto.Request
                {
                    DeliveryBillId = deliveryBillId,
                    PrintoutTypeId = printoutTypeId
                };

                billFile = oneCApiClient.GetBillFile(request);
            }
            catch (Exception e)
            {
                // TODO: Log
                Console.WriteLine(e);
            }

            byte[] fileBytes = null;
            if (billFile != null && billFile.ErrorCode == 0)
                fileBytes = Convert.FromBase64String(billFile.Base64Content);

            if (fileBytes != null)
            {
                var fileName = $@"{printoutTypeId}_{DateTime.Now:yyyy-MM-dd-h24-mm-ss}.pdf";
                return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult GetLabCertificate(long ticketId, string returnUrl)
        {
            var file = _context.TicketFiles.FirstOrDefault(item =>
                item.TicketId == ticketId && item.TypeId == Dom.TicketFile.Type.LabCertificate);
            if (file != null)
            {
                var fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
                var fileName = file.Name;
                return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
            }

            return Redirect(returnUrl);
        }
        
        [HttpGet]
        public ActionResult LabAverageRates(DateTime? date)
        {
            if (date is null) date = DateTime.Today;
            var vm = _ticketWebManager.GetLabAverageRatesVm(date.Value);

            return View("LabAverageRates", vm);
        }
    }
}