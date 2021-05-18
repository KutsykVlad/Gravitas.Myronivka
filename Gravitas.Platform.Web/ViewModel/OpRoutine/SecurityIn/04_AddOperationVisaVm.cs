using System;
using System.ComponentModel;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.OpData;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SecurityInVms {

		public class AddOperationVisaVm {
			public long NodeId { get; set; }

			public string Rfid { get; set; }
			public DateTime ReadTime { get; set; }
			
			public BasicTicketContainerData TruckBaseInfo { get; set; }
			[DisplayName("Власний транспорт(авто)")]
			public string OwnTruck { get; set; }
			[DisplayName("Власний транспорт(причіп)")]
			public string OwnTrailer { get; set; }
		}
	}
}