using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class ShowTicketMenuVm
        {
            public long NodeId { get; set; }
            public long TicketContainerId { get; set; }
            public long TicketId { get; set; }
            public long TicketStatusId { get; set; }

            [DisplayName("Код поставки")]
            [Required(ErrorMessage = "Поле повинне бути заповненим.")]
            [RegularExpression(@"\S+", ErrorMessage = "Хибний формат стрічки")]
            public string SupplyCode { get; set; }
            
            public int NewWeightValue { get; set; }

            public string DeliveryBillId { get; set; }

            [DisplayName("Транспорт No.")] 
            public string TransportNo { get; set; }

            [DisplayName("Причеп No.")] 
            public string TrailerNo { get; set; }

            [DisplayName("Перевізник")] 
            public bool IsThirdPartyCarrier { get; set; }

            [DisplayName("Картка")]
            public string CardNumber { get; set; }

            [DisplayName("Номенклатура")] 
            public string Nomenclature { get; set; }

            public bool IsPhoneNumberAvailable { get; set; }

            public bool IsEditable { get; set; }
        }
    }
}