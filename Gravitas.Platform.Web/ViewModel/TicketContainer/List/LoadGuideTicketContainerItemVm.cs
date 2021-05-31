using System.ComponentModel;
using Gravitas.Platform.Web.Manager.TicketContainer;

namespace Gravitas.Platform.Web.ViewModel
{
    public class LoadGuideTicketContainerItemVm
    {
        public BaseRegistryData BaseData { get; set; }

        [DisplayName("Точка завантаження")]
        public int LoadNodeId { get; set; }
        
        [DisplayName("Точка завант.")] 
        public string LoadNodeName { get; set; }

        [DisplayName("Норматив погрузки, кг.")]
        public double LoadTarget { get; set; }

        [DisplayName("Плюс, кг.")] 
        public int LoadTargetDeviationPlus { get; set; }

        [DisplayName("Мінус, кг.")] 
        public int LoadTargetDeviationMinus { get; set; }

        public bool IsActive { get; set; }
        public bool CanInvite { get; set; }
    }
}