using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Platform.Web.ViewModel.Node.List;

namespace Gravitas.Platform.Web.ViewModel {

	public class NodeItemsVm {

		public ICollection<NodeItemVm> Items { get; set; }
		public int Count { get; set; }
	}
}
