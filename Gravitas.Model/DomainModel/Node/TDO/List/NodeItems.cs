using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class NodeItems {

		public ICollection<NodeItem> Items { get; set; }
		public int Count { get; set; }
	}
}
