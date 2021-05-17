using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {
    
    public class LabFacelessTicketContainerListVm
	{
        public ICollection<LabFacelessTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}