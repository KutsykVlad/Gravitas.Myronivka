namespace Gravitas.Model.Dto
{
    public class OrganizationUnitDetail : BaseEntity<long>
    {
        public long UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }

        public string Name { get; set; }

        public NodeItems NodeItems { get; set; }
        public OrganizationUnitItems ChildItems { get; set; }
    }
}