using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.OrganizationUnit.DAO;
using OrganizationUnit = Gravitas.Model.DomainValue.OrganizationUnit;

namespace Gravitas.Model {

	public partial class OrganizationUnitTypeVm {

		
		public string Name { get; set; }
 
		public virtual ICollection<OrganizationUnit> OrganizationUnitSet { get; set; }
	}
}
