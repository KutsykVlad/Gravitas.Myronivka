using System.Collections.Generic;
using System.Linq;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class DriverCheckInListVm
    {
        public IEnumerable<DriverCheckInItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}