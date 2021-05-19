using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Shared;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;

namespace Gravitas.Platform.Web.ViewModel
{
    public class CentralLabTicketContainerListVm
    {
        public ICollection<CentralLabTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}