using System;

namespace Gravitas.Platform.Web.ViewModel{

	public class NodeContextVm {

		public long? OpRoutineStateId { get; set; }
		public long? TicketContainerId { get; set; }
		public long? TicketId { get; set; }
		public Guid? OpDataId { get; set; }
		public long? OpDataComponentId { get; set; }

		public DateTime? LastStateChangeTime { get; set; }
	}
}

