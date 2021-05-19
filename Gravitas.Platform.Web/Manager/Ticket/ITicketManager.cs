using System;
using System.Web;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.LabAverageRates;
using Gravitas.Platform.Web.ViewModel.Ticket.History;
using SingleWindowVms = Gravitas.Platform.Web.ViewModel.OpRoutine.SingleWindow.SingleWindowVms;

namespace Gravitas.Platform.Web.Manager.Ticket
{
    public interface ITicketWebManager
    {
        TicketItemsVm GetTicketItemsVm(int containerId);
        SingleWindowVms.GetTicketVm GetTicketVm(int nodeId);
        bool UploadFile(int nodeId, HttpPostedFileBase upload, TicketFileType typeId = TicketFileType.Other);
        bool SendReplySms(int nodeId);
        TicketHistoryItems GetHistoryList(int pageNumber, int pageSize, string query, DateTime? date);
        TicketHistoryDetails GetHistoryDetails(int ticketId, int ticketContainerId);
        LabAverageRatesItems GetLabAverageRatesVm(DateTime date);
    }
}