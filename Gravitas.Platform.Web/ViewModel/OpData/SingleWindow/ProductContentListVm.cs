using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SingleWindowVms {

		public class ProductContentListVm {

			public ICollection<ProductContentItemVm> Items { get; set; }
			public int Count { get; set; }
		}
	}
}
