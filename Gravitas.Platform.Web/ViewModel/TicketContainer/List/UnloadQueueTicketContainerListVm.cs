using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class UnloadQueueTicketContainerListVm
    {
        public ICollection<UnloadQueueTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}