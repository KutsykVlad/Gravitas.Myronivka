using System;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class UnloadCheckPointVms {

		public class IdleVm {
			public long NodeId { get; set; }
			public LoadPointTicketContainerItemVm BindedTruck { get; set; }
		}
	}
}