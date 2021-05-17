using System.Collections.Generic;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel {

	public class NodeItemsVm {

		public ICollection<NodeItemVm> Items { get; set; }
		public int Count { get; set; }
	}
}
