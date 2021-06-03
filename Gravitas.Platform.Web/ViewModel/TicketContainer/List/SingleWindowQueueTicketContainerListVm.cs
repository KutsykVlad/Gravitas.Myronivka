using System.Collections.Generic;
using System.Linq;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class SingleWindowQueueTicketContainerListVm
    {
        public IEnumerable<IGrouping<string, SingleWindowTicketContainerItemVm>> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}