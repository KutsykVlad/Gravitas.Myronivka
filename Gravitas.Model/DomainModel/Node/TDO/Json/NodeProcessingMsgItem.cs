using System;

namespace Gravitas.Model.Dto {

	public class NodeProcessingMsgItem : BaseJsonConverter<NodeProcessingMsgItem> {
		public DateTime? Time { get; set; }
		public int TypeId { get; set; }
		public string Text { get; set; }

		public NodeProcessingMsgItem(int typeId, string text ) {
			Time = DateTime.Now;
			TypeId = typeId;
			Text = text;
		}
	}
}