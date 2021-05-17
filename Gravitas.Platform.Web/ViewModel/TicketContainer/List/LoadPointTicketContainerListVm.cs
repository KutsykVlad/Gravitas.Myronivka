using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

    public class LoadPointTicketContainerListVm {

		public ICollection<LoadPointTicketContainerItemVm> Items { get; set; }
		public ActionLinkVm DetailActionLink { get; set; }
	}
}