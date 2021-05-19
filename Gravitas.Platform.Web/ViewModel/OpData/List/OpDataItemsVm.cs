using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.OpData.List;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public class OpDataItemsVm {

		public OpDataItemsVm() {
			Items = new List<OpDataItemVm>();
		}

		public List<OpDataItemVm> Items { get; set; }
		public int Count { get; set; }
		public bool ShowPhotoIcons { get; set; }
		public bool ShowFullPhotos { get; set; }
	}
}