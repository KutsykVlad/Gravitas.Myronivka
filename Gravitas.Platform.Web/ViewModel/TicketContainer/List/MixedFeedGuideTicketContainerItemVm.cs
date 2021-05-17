using System.ComponentModel;
using Gravitas.Platform.Web.Manager;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class MixedFeedGuideTicketContainerItemVm
    {
        public BaseRegistryData BaseData { get; set; }

        [DisplayName("Проїзд завантаження")]
        public long LoadGateId { get; set; }

        [DisplayName("Точка завант.")]
        public string LoadNodeName { get; set; }

        [DisplayName("Ост. опрацьована точка")]
        public string LastNodeName { get; set; }

        public bool IsActive { get; set; }
        public bool CanInvite { get; set; }
    }
}