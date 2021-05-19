using System;
using System.Web.Mvc;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Report;

namespace Gravitas.Platform.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportWebManager _reportWebManager;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly INodeRepository _nodeRepository;

        public ReportController(IReportWebManager reportWebManager,
            IOpRoutineWebManager opRoutineWebManager,
            INodeRepository nodeRepository)
        {
            _reportWebManager = reportWebManager;
            _opRoutineWebManager = opRoutineWebManager;
            _nodeRepository = nodeRepository;
        }

        public FileResult Generate(Guid id, long nodeId)
        {
            SignalRInvoke.StartSpinner(nodeId);
            var templateUri = Server.MapPath("~/Content/reports/labReportTemplate.html");

            var vm = _opRoutineWebManager.LaboratoryIn_SamplePrintout_GetVm(id);
            if (vm == null) templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");

            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Landscape, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Report.pdf");
        }

        public FileResult GenerateCentralLabReport(int nodeId)
        {
            SignalRInvoke.StartSpinner(nodeId);
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null) return null;
            var templateUri = Server.MapPath("~/Content/reports/centralLabReportTemplate.html");

            var vm = _opRoutineWebManager.CentralLaboratory_GetSamplePrintoutVm(nodeDto.Context.TicketId.Value);
            if (vm == null) templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");

            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Portrait, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "CentralLaboratoryReport.pdf");
        }

        public FileResult GenerateCentralLabLabel(Guid id, int nodeId)
        {
            SignalRInvoke.StartSpinner(nodeId);
            var templateUri = Server.MapPath("~/Content/reports/centralLabReportLabel.html");

            var vm = _opRoutineWebManager.CentralLaboratory_GetLabelPrintoutVm(id);
            if (vm == null) templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");

            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Portrait, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "CentralLaboratoryLabel.pdf");
        }

        public FileResult GenerateProtocol(long nodeId, long? ticketId = null)
        {
            SignalRInvoke.StartSpinner(nodeId);
            var templateUri = Server.MapPath("~/Content/reports/ProtocolTemplate.html");

            var vm = _opRoutineWebManager.SingleWindow_ProtocolPrintout_GetVm(nodeId, ticketId);

            if (vm == null)
            {
                templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");
            }
            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Portrait, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Protocol.pdf");
        }

        public FileResult GenerateSiloProtocol(long nodeId)
        {
            SignalRInvoke.StartSpinner(nodeId);
            var templateUri = Server.MapPath("~/Content/reports/mixedFeedSiloReport.html");

            var vm = _opRoutineWebManager.MixedFeed_ProtocolPrintout_GetVm(nodeId);

            if (vm == null)
            {
                templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");
            }
            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Landscape, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "MixedFeedProtocol.pdf");
        }

        public FileResult GenerateRouteReport(long nodeId, long ticketId)
        {
            SignalRInvoke.StartSpinner(nodeId);
            var templateUri = Server.MapPath("~/Content/reports/routeTemplate.html");

            var vm = _opRoutineWebManager.SingleWindow_RoutePrintout_GetVm(nodeId, ticketId);

            if (vm == null)
            {
                templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");
            }
            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Portrait, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Route.pdf");
        }

        public FileResult GenerateTechnicalRouteReport(long nodeId)
        {
           SignalRInvoke.StartSpinner(nodeId); var templateUri = Server.MapPath("~/Content/reports/technologicalRouteTemplate.html");
            var vm =  _opRoutineWebManager.SingleWindow_TechnologicalRoutePrintout_GetVm(nodeId);

            if (vm == null)
            {
                templateUri = Server.MapPath("~/Content/reports/errorTemplate.html");
            }
            SignalRInvoke.StopSpinner(nodeId);
            return File(_reportWebManager.GenerateReportById(vm, templateUri, PdfOrientation.Portrait, "A4").ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "BillFile.pdf");


        }
    }
}
