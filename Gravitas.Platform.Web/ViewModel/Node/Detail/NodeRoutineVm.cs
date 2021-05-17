namespace Gravitas.Platform.Web.ViewModel {

	public class NodeRoutineVm {

		public long NodeId { get; set; }
		public long WorkstationId { get; set; }
		public string NodeCode { get; set; }
		public string NodeName { get; set; }

		public NodeContextVm NodeContextVm { get; set; }
		public NodeProcessingMsgVm NodeProcessingMsgVm { get; set; }
	}
}