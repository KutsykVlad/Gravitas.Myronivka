using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class TicketItems : BaseEntity<long> {
		public TicketItems() {
			Items = new List<TicketItem>();
		}

		public ICollection<TicketItem> Items { get; set; }
		public int Count { get; set; }
	}
}
