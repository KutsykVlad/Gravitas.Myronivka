using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class TicketHistoryItem
    {
        public long TicketId { get; set; }
        public long TicketContainerId { get; set; }
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
        [DisplayName("Відправник")]
        public string PartnerName { get; set; }
        [DisplayName("Дата опрацювання")]
        public string EditDateTime { get; set; }
    }
}