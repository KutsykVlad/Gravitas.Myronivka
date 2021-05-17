using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

    public class UnloadPointTicketContainerListVm {

		public ICollection<UnloadPointTicketContainerItemVm> Items { get; set; }
		public ActionLinkVm DetailActionLink { get; set; }
	}
}