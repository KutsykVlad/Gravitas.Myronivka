using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OrganizationUnit.DAO
{
    public class OrganizationUnit : BaseEntity<int>
    {
        public OrganizationUnit()
        {
            NodeSet = new HashSet<Node.DAO.Node>();
        }

        public int UnitTypeId { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public virtual OrganizationUnitType UnitType { get; set; }
        public virtual ICollection<Node.DAO.Node> NodeSet { get; set; }
    }
}