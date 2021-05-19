using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.OpData.Base
{
    public class BaseOpDataDetailVm
    {
        public Guid Id { get; set; }
        public int? NodeId { get; set; }
        public int? TicketId { get; set; }
        public int? StateId { get; set; }
        [DisplayName("Час внесення проби")] public DateTime? CheckInDateTime { get; set; }
        public DateTime? CheckOutDateTime { get; set; }
    }
}