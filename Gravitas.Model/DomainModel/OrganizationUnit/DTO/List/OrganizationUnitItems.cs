using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.OrganizationUnit.DTO.List
{
    public class OrganizationUnitItems
    {
        public IEnumerable<OrganizationUnitItem> Items { get; set; }
        public int Count { get; set; }
    }
}