using System.Collections.Generic;

namespace Gravitas.Model {
	
	public partial class TicketContainerStatus : BaseEntity<long> {

		public TicketContainerStatus() {
			TicketContainerSet = new HashSet<TicketContainer>();
		}

		public string Name { get; set; }

		// Navigation Properties
		public virtual ICollection<TicketContainer> TicketContainerSet { get; set; }
	}
}
