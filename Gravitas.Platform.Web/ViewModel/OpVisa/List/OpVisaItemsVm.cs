using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Platform.Web.ViewModel {
	
	public class OpVisaItemsVm : BaseEntity<int> {

		public List<OpVisaItemVm> Items { get; set; }
	}
}
