using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel
{
    public class LoadGuideTicketContainerListVm
    {
        public ICollection<LoadGuideTicketContainerItemVm> Items { get; set; }
        public int BudgeCount { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}