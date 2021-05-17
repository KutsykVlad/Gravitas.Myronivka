using System;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SecurityInVms {

		public class OpenBarrierVm {
			public long NodeId { get; set; }
			
			public bool OpenBarrierState { get; set; }
		}
	}
}