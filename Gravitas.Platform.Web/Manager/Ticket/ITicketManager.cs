using System;
using System.Web;
using Gravitas.Model;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.LabAverageRates;
using Gravitas.Platform.Web.ViewModel.Ticket.History;

namespace Gravitas.Platform.Web.Manager
{
    public interface ITicketWebManager
    {
        TicketItemsVm GetTicketItemsVm(long containerId);
        SingleWindowVms.GetTicketVm GetTicketVm(long nodeId);
        bool UploadFile(long nodeId, HttpPostedFileBase upload, int typeId = Dom.TicketFile.Type.Other);
        bool SendReplySms(long nodeId);
        TicketHistoryItems GetHistoryList(int pageNumber, int pageSize, string query, DateTime? date);
        TicketHistoryDetails GetHistoryDetails(long ticketId, long ticketContainerId);
        LabAverageRatesItems GetLabAverageRatesVm(DateTime date);
    }
}