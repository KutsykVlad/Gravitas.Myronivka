using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class OrganizationUnitItems {

		public IEnumerable<OrganizationUnitItem> Items { get; set; }
		public int Count { get; set; }
	}
}
