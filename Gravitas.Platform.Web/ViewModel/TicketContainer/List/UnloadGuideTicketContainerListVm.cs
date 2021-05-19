using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel {

    public class UnloadGuideTicketContainerListVm {

		public ICollection<UnloadGuideTicketContainerItemVm> Items { get; set; }
		public ActionLinkVm DetailActionLink { get; set; }
	    public int BudgeCount { get; set; }
    }
}