namespace Gravitas.Platform.Web.ViewModel
{
	public class NodeProgresVm {

		public string NodeName { get; set; }
		public string NodeCode { get; set; }
		
		public string CurrentStatusName { get; set; }
		public int? CurrentStatusIndex { get; set; }
		public int? StatusCount { get; set; }
	}
}