using System.Collections.Generic;

namespace Gravitas.Model.Dto {
	
	public class OpVisaItems : BaseEntity<long> {

		public List<OpVisaItem> Items { get; set; }
	}
}
