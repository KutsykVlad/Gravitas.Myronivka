using System.Collections.Generic;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel
{
    public class SingleWindowTicketContainerListVm
    {
        public ICollection<SingleWindowTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }

        public long SelectedFilterId { get; set; }
        public IEnumerable<SelectListItem> FilterItems { get; set; }
    }
}