using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class LoadGuideTicketContainerListVm
    {
        public ICollection<LoadGuideTicketContainerItemVm> Items { get; set; }
        public int BudgeCount { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}