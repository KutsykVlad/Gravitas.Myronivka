using System.ComponentModel;
using Gravitas.Platform.Web.Manager;

namespace Gravitas.Platform.Web.ViewModel {
    
    public class SingleWindowTicketContainerItemVm {
        
        [DisplayName("Ост. опрацьована точка")]
        public string NodeName { get; set; }
        public int TruckState { get; set; }
        public BaseRegistryData BaseData { get; set; }
    }
}