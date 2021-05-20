using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class GetTicketVm
        {
            public int NodeId { get; set; }
            public int TicketContainerId { get; set; }

            [DisplayName("Код поставки або Бар-код")]
            [Required(ErrorMessage = "Поле повинне бути заповненим.")]
            [RegularExpression(@"\S+", ErrorMessage = "Хибний формат стрічки")]
            public string SupplyBarCode { get; set; }

            public TicketItemsVm TicketList { get; set; }

            [DisplayName("Транспорт No.")] 
            public string TransportNo { get; set; }
            [DisplayName("Причеп No.")] 
            public string TrailerNo { get; set; }
            [DisplayName("Перевізник")] 
            public bool IsThirdPartyCarrier { get; set; }
            [DisplayName("Картка")] 
            public string CardNumber { get; set; }

            public bool IsEditable { get; set; }
        }
    }
}