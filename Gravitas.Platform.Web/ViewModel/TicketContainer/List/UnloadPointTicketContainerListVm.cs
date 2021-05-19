using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel {

    public class UnloadPointTicketContainerListVm {

		public ICollection<UnloadPointTicketContainerItemVm> Items { get; set; }
		public ActionLinkVm DetailActionLink { get; set; }
	}
}