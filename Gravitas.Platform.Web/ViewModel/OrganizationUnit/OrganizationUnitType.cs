using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model {

	public partial class OrganizationUnitTypeVm {

		
		public string Name { get; set; }
 
		public virtual ICollection<OrganizationUnit> OrganizationUnitSet { get; set; }
	}
}
