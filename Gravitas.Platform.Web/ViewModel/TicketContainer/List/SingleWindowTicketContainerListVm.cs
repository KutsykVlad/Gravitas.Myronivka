using System.Collections.Generic;
using System.Web.Mvc;
using Gravitas.Platform.Web.ViewModel.Shared;

namespace Gravitas.Platform.Web.ViewModel
{
    public class SingleWindowTicketContainerListVm
    {
        public ICollection<SingleWindowTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }

        public int SelectedFilterId { get; set; }
        public IEnumerable<SelectListItem> FilterItems { get; set; }
    }
}