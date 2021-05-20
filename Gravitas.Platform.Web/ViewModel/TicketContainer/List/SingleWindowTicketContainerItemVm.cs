using System.ComponentModel;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.TicketContainer;

namespace Gravitas.Platform.Web.ViewModel
{
    public class SingleWindowTicketContainerItemVm
    {
        [DisplayName("Ост. опрацьована точка")]
        public string NodeName { get; set; }

        public TruckState TruckState { get; set; }
        public BaseRegistryData BaseData { get; set; }
    }
}