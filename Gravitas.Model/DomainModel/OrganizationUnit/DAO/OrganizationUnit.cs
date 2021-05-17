using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.DAO;

namespace Gravitas.Model {

	public partial class OrganizationUnit : BaseEntity<int> {

		public OrganizationUnit() {
			NodeSet = new HashSet<Node>();
		}

		public long UnitTypeId { get; set; }
		public string Name { get; set; }
 
		// Navigation Properties
		public virtual OrganizationUnitType UnitType { get; set; }
		public virtual ICollection<Node> NodeSet { get; set; }
	}
}
