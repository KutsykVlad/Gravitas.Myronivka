using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

    public class MixedFeedLoadTicketContainerListVm {

		public ICollection<MixedFeedLoadTicketContainerItemVm> Items { get; set; }
		public ActionLinkVm DetailActionLink { get; set; }
	}
}