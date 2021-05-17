using Gravitas.Model;
using Gravitas.Model.DomainModel.Base;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public class OrganizationUnitItemVm : BaseEntity<int> {

		public string Name { get; set; }
		public int ChildCount { get; set; }
		public int NodeCount { get; set; }
	}
}
