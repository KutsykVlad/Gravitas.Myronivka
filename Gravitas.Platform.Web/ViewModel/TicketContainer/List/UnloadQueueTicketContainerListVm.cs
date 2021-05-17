using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class UnloadQueueTicketContainerListVm
    {
        public ICollection<UnloadQueueTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}