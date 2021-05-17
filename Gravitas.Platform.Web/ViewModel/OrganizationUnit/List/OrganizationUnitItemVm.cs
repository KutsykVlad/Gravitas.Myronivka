using Gravitas.Model;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public class OrganizationUnitItemVm : BaseEntity<long> {

		public string Name { get; set; }
		public int ChildCount { get; set; }
		public int NodeCount { get; set; }
	}
}
