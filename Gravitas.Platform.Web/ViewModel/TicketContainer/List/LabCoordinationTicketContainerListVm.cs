using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel {
    
    public class LabCoordinationTicketContainerListVm {
        
        public ICollection<LabCoordinationTicketContainerItemVm> Items { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}