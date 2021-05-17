﻿namespace Gravitas.Model.Dto {

	public static partial class ExternalData {
		public class ProductDetail : BaseEntity<string> {

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
		}
	}
}