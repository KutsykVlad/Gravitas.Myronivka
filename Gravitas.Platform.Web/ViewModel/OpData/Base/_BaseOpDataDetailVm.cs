using System;
using System.ComponentModel;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel.OpData.Base
{
    public class BaseOpDataDetailVm
    {
        public Guid Id { get; set; }
        public int? NodeId { get; set; }
        public int? TicketId { get; set; }
        public OpDataState? StateId { get; set; }
        [DisplayName("Час внесення проби")] 
        public DateTime? CheckInDateTime { get; set; }
        public DateTime? CheckOutDateTime { get; set; }
    }
}