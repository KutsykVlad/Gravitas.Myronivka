using System.Collections.Generic;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel {
	
	public class OpVisaItemsVm : BaseEntity<long> {

		public List<OpVisaItemVm> Items { get; set; }
	}
}
