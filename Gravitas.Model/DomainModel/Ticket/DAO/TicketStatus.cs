using System.Collections.Generic;

namespace Gravitas.Model {
	
	public partial class TicketStatus : BaseEntity<long> {

		public TicketStatus() {
			TicketSet = new HashSet<Ticket>();
		}

		public string Name { get; set; }

		// Navigation Properties
		public virtual ICollection<Ticket> TicketSet { get; set; }
	}
}
