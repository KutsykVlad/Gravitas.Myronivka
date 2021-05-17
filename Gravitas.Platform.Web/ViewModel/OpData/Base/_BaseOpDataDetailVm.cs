using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel {

	public class BaseOpDataDetailVm {
		
		public Guid Id { get; set; }
		public long? NodeId { get; set; }
		public long? TicketId { get; set; }
		public long? StateId { get; set; }
	    [DisplayName("Час внесення проби")]
        public DateTime? CheckInDateTime { get; set; }
        public DateTime? CheckOutDateTime { get; set; }
	}
}
