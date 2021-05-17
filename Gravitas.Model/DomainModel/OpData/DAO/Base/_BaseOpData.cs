using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {

	public class BaseOpData : BaseEntity<Guid> {
		
		public BaseOpData() {
			OpVisaSet = new HashSet<OpVisa>();
			OpCameraSet = new HashSet<OpCameraImage>();
		}

		public long StateId { get; set; }

		public long? NodeId { get; set; }
		public long? TicketId { get; set; }
		public long? TicketContainerId { get; set; }
		
		public DateTime? CheckInDateTime { get; set; }
		public DateTime? CheckOutDateTime { get; set; }
		
		// Navigation properties
		public virtual Node Node { get; set; }
		public virtual Ticket Ticket { get; set; }
		public virtual OpDataState OpDataState { get; set; }
		public virtual ICollection<OpVisa> OpVisaSet { get; set; }
		public virtual ICollection<OpCameraImage> OpCameraSet { get; set; }
	}
}
