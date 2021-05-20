using System;
using Gravitas.Platform.Web.ViewModel.Device._Base;

namespace Gravitas.Platform.Web.ViewModel {
	
	public class OpVisaItemVm : BaseEntityVm<long> {

		public DateTime? DateTime { get; set; }
		public string Message { get; set; }
		public string UserName { get; set; }
	}
}
