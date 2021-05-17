using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

	public class NodeProcessingMsgVm {

		public ICollection<NodeProcessingMsgItemVm> Items { get; set; }

		public NodeProcessingMsgVm() {
			Items = new List<NodeProcessingMsgItemVm>();
		}
	}
}