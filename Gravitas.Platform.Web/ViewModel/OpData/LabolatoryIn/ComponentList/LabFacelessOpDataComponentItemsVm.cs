using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public static partial class LaboratoryInVms {

		public class LabFacelessOpDataComponentItemsVm {
		
			public LabFacelessOpDataComponentItemsVm() {
				Items = new List<LabFacelessOpDataComponentItemVm>();
			}

			public ICollection<LabFacelessOpDataComponentItemVm> Items { get; set; }
			public int Count { get; set; }
		}
	}
}