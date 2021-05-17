using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OrganizationUnit.DTO.List
{
    public class OrganizationUnitItem : BaseEntity<int>
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int ChildCount { get; set; }
        public int NodeCount { get; set; }
    }
}