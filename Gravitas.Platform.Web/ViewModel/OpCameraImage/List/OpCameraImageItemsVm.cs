using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

	public class OpCameraImageItemsVm {

		public OpCameraImageItemsVm() {
			Items = new List<OpCameraImageItemVm>();
		}

		public IList<OpCameraImageItemVm> Items { get; set; }
		public int Count { get; set; }
	}
}