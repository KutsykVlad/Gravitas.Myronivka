using System.ComponentModel;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.TicketContainer;

namespace Gravitas.Platform.Web.ViewModel {
    
    public class LabFacelessTicketContainerItemVm {
        
        [DisplayName("Коментар лабораторії")]
        public string Comment { get; set; }
        
        [DisplayName("Стан")]
        public string State { get; set; }
        
        public bool IsReadyToManage { get; set; }
        public BaseRegistryData BaseData { get; set; }
    }
}