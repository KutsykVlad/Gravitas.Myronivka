namespace Gravitas.Platform.Web.ViewModel
{
	public class NodeProgresVm {

		public string NodeName { get; set; }
		public string NodeCode { get; set; }
		
		public string CurrentStatusName { get; set; }
		public long? CurrentStatusIndex { get; set; }
		public long? StatusCount { get; set; }
	}
}