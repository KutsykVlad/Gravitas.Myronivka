using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public class OrganizationUnitItemsVm {

		public ICollection<OrganizationUnitItemVm> Items { get; set; }
		public int Count { get; set; }
	}
}
