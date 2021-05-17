using System.Collections.Generic;

namespace Gravitas.Model {

	public partial class OrganizationUnit : BaseEntity<long> {

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
