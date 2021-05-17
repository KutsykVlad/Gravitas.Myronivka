using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {
	
	public partial class TicketStatus : BaseEntity<int> {

		public TicketStatus() {
			TicketSet = new HashSet<Ticket>();
		}

		public string Name { get; set; }

		// Navigation Properties
		public virtual ICollection<Ticket> TicketSet { get; set; }
	}
}
