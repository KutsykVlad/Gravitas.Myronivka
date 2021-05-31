using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class ProtocolPrintoutVm
        {
            public int? TicketId { get; set; }
            [DisplayName("Транспорт No.")]
            public string TransportNo { get; set; }
            [DisplayName("Причеп No.")]
            public string TrailerNo { get; set; }
            [DisplayName("Перевізник")]
            public string IsThirdPartyCarrier { get; set; }
            [DisplayName("Картка")]
            public string CardNumber { get; set; }
            [DisplayName("Номенклатура")]
            public string Nomenclature { get; set; }

            public string DeliveryBillCode { get; set; }
            public string PartnerName { get; set; }
            public string Comments { get; set; }
            public string ReceiverName { get; set; }
            public DateTime? EntranceTime { get; set; }

            public DateTime? ExitTime { get; set; }

            public OpDataItemsVm OpDataItems { get; set; }

        }
    }
}