using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Gravitas.Model.DomainModel.OrganizationUnit.DTO.List;
using Gravitas.Model.Dto;

namespace Gravitas.Model.DomainModel.OrganizationUnit.DTO.Detail
{
    public class OrganizationUnitDetail : BaseEntity<int>
    {
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
        public string Name { get; set; }

        public NodeItems NodeItems { get; set; }
        public OrganizationUnitItems ChildItems { get; set; }
    }
}