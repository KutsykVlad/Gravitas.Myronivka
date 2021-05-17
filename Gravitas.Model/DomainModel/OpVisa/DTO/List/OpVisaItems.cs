using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {
	
	public class OpVisaItems : BaseEntity<int> {

		public List<OpVisaItem> Items { get; set; }
	}
}
