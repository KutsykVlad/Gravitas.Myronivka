using Gravitas.Model.DomainModel.Base;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel
{
    public class OrganizationUnitDetailVm : BaseEntity<int>
    {
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }

        public string Name { get; set; }

        public NodeItemsVm NodeItems { get; set; }
        public OrganizationUnitItemsVm ChildItems { get; set; }
    }
}