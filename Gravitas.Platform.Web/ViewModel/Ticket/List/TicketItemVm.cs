using System.ComponentModel;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Platform.Web.ViewModel.Device._Base;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Platform.Web.ViewModel
{
    public class TicketItemVm : BaseEntityVm<int>
    {
        [DisplayName("ID")] 
        public TicketStatus StatusId { get; set; }
        [DisplayName("Статус")] 
        public string StatusName { get; set; }
        [DisplayName("Код поставки")] 
        public string SupplyCode { get; set; }
        [DisplayName("Номенклатура")] 
        public string Product { get; set; }
    }
}