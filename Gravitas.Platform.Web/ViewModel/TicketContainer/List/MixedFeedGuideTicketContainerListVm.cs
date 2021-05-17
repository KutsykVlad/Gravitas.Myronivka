using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class MixedFeedGuideTicketContainerListVm
    {
        public ICollection<MixedFeedGuideTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
        public int BudgeCount { get; set; }
    }
}