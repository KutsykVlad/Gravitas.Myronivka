using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.TDO.List;

namespace Gravitas.Model.Dto
{
    public class OrganizationUnitDetail : BaseEntity<int>
    {
        public long UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }

        public string Name { get; set; }

        public NodeItems NodeItems { get; set; }
        public OrganizationUnitItems ChildItems { get; set; }
    }
}