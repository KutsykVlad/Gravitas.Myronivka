using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SecurityOutVms {

		public class ShowOperationsListVm {
			public long NodeId { get; set; }
			public List<long> Tickets { get; set; }

			public string TruckNo { get; set; }
			public string TrailerNo { get; set; }
			public bool IsTechRoute { get; set; }
		}
	}
}