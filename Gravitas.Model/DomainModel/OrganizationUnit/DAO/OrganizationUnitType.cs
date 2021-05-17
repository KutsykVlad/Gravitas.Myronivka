using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {

	[Table("OrganizationUnitType")]
	public partial class OrganizationUnitType : BaseEntity<int> {

		public OrganizationUnitType() {
			OrganizationUnitSet = new HashSet<OrganizationUnit>();
		}
		
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
 
		public virtual ICollection<OrganizationUnit> OrganizationUnitSet { get; set; }
	}
}
