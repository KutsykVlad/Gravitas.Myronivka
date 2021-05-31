using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.Ticket.History
{
    public class TicketHistoryDetails
    {
        public string DeliveryBillId { get; set; }
        public string ReturnUrl { get; set; }
        public int TicketId { get; set; }
        public int TicketContainerId { get; set; }
        [DisplayName("Картка")]
        public string CardNumber { get; set; }
        [DisplayName("Транспорт")]
        public string TransportNo { get; set; }
        [DisplayName("Причеп")]
        public string TrailerNo { get; set; }
        [DisplayName("Номенклатура")]
        public string ProductName { get; set; }
        [DisplayName("Перевізник")]
        public bool IsThirdPartyCarrier { get; set; }
        [DisplayName("Ост. опрацьована точка")]
        public string NodeName { get; set; }
    }
}