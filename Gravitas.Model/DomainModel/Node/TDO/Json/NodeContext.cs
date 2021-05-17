using System;
using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class NodeContext : BaseJsonConverter<NodeContext> {

		public long? OpRoutineStateId { get; set; }
		public long? TicketContainerId { get; set; }
		public long? TicketId { get; set; }
		public Guid? OpDataId { get; set; }
		public long? OpDataComponentId { get; set; }
		public long? OpProcessData{ get; set; }

		public DateTime? LastStateChangeTime { get; set; }
	}
}

