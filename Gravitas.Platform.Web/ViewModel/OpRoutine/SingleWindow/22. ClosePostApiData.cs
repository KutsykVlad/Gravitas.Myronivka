﻿using System;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SingleWindowVms {

		public class ClosePostApiDataVm {
			public long NodeId { get; set; }
			public long OpDataId { get; set; }
			public string SupplyCode { get; set; }
			public DateTime RequestSentDateTime { get; set; }
		}
	}
}