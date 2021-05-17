﻿using System;

namespace Gravitas.Model {

	public static partial class ExternalData {

		public class Contract : BaseEntity<string> {

			public string Code { get; set; }
			public string Name { get; set; }
			public DateTime? StartDateTime { get; set; }
			public DateTime? StopDateTime { get; set; }
			public string ManagerId { get; set; }
		}
	}
}