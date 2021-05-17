using Gravitas.Model;
using Gravitas.Model.DomainModel.Base;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public class OrganizationUnitDetailVm : BaseEntity<int> {

		public long UnitTypeId { get; set; }
		public string UnitTypeName { get; set; }

		public string Name { get; set; }

		public NodeItemsVm NodeItems { get; set; }
		public OrganizationUnitItemsVm ChildItems { get; set; }
	}
}
