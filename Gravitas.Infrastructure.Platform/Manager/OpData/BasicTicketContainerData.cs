using System.ComponentModel;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public class BasicTicketContainerData
    {
        [DisplayName("Номенклатура")]
        public string ProductName { get; set; }

        [DisplayName("Відправник")]
        public string SenderName { get; set; }

        [DisplayName("Транспорт")]
        public string TransportNo { get; set; }

        [DisplayName("Причіп")]
        public string TrailerNo { get; set; }

        [DisplayName("Перевізник")]
        public bool IsThirdPartyCarrier { get; set; }

        [DisplayName("Коментар")]
        public string SingleWindowComment { get; set; }

        public string DeliveryBillCode { get; set; }

        [DisplayName("Пломби")]
        public string StampList { get; set; }

        [DisplayName("Норматив погрузки, кг.")]
        public double LoadTarget { get; set; }

        [DisplayName("Склад контрагента")]
        public string ReceiverDepotName { get; set; }

        public bool IsTechRoute { get; set; }
    }
}