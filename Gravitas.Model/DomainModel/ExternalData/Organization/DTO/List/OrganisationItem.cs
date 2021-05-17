using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public static partial class ExternalData  {
		public class OrganisationItems : BaseEntity<string> {

			public IEnumerable<OrganisationItem> Items { get; set; }
		}
	}

}
