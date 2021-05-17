using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {
	
	public partial class TicketContainerStatus : BaseEntity<int> {

		public TicketContainerStatus() {
			TicketContainerSet = new HashSet<TicketContainer>();
		}

		public string Name { get; set; }

		// Navigation Properties
		public virtual ICollection<TicketContainer> TicketContainerSet { get; set; }
	}
}
