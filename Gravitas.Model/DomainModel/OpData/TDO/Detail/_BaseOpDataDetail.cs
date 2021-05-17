using System;

namespace Gravitas.Model.Dto {

	public class BaseOpDataDetail {
		
		public Guid Id { get; set; }

		public long StateId { get; set; }

		public long? NodeId { get; set; }
		public long? TicketId { get; set; }
		public long? TicketContainerId { get; set; }
		
		public DateTime? CheckInDateTime { get; set; }
		public DateTime? CheckOutDateTime { get; set; }
	}
}
