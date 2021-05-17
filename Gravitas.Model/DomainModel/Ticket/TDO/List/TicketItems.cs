using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public class TicketItems : BaseEntity<int> {
		public TicketItems() {
			Items = new List<TicketItem>();
		}

		public ICollection<TicketItem> Items { get; set; }
		public int Count { get; set; }
	}
}
