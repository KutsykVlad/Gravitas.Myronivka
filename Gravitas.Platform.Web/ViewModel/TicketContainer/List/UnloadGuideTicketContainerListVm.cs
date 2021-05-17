using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {

    public class UnloadGuideTicketContainerListVm {

		public ICollection<UnloadGuideTicketContainerItemVm> Items { get; set; }
		public ActionLinkVm DetailActionLink { get; set; }
	    public int BudgeCount { get; set; }
    }
}