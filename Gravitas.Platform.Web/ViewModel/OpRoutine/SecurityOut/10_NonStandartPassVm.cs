using System;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SecurityOutVms {

		public class NonStandartPassVm {

			public long NodeId { get; set; }

			public string Rfid { get; set; }
			public DateTime ReadTime { get; set; }
		}
	}
}