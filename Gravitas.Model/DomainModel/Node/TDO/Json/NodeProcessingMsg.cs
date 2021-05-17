using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class NodeProcessingMsg : BaseJsonConverter<NodeProcessingMsg> {

		public ICollection<NodeProcessingMsgItem> Items { get; set; }

		public NodeProcessingMsg() {
			Items = new List<NodeProcessingMsgItem>();
		}
	}
}