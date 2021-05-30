using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SingleWindowVms {

		public class ProductContentItemVm {

			[DisplayName("No.")]
			public int No { get; set; }
			[DisplayName("Заявка")]
			public string OrderNumber { get; set; }
			public Guid? ProductId { get; set; }
			[DisplayName("Продукт")]
			public string ProductName { get; set; }
			public Guid? UnitId { get; set; }
			[DisplayName("Од. виміру")]
			public string UnitName { get; set; }
			[DisplayName("Кількість")]
			public double Quantity { get; set; }
		}
	}
}
