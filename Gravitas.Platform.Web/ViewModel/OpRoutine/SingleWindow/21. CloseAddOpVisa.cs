﻿using System;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SingleWindowVms {

		public class CloseAddOpVisaVm
		{
			public long NodeId { get; set; }

			public string Rfid { get; set; }
			public DateTime ReadTime { get; set; }
		}
	}
}