using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class PreRegisterTicketContainerListVm
    {
        public ICollection<PreRegisterTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}