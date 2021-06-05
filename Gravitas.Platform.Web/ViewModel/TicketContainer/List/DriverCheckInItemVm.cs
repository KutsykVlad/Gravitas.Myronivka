using System.ComponentModel;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.TicketContainer;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class DriverCheckInItemVm
    {
        [DisplayName("Порядковий номер")]
        public int OrderNumber { get; set; }

        [DisplayName("Картка")]
        public string CardNumber { get; set; }
        
        [DisplayName("Авто")]
        public string Truck { get; set; }

        [DisplayName("Причіп")]
        public string Trailer { get; set; }
        
        [DisplayName("Продукт")]
        public string Product { get; set; }

        [DisplayName("Номер телефону")]
        public string PhoneNumber { get; set; }
        
        [DisplayName("Стан")]
        public bool HasTicket { get; set; }
    }
}