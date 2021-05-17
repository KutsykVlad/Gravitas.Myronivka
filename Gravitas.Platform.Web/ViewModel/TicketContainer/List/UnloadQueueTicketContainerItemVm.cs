using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class UnloadQueueTicketContainerItemVm
    {
        public long TicketContainerId { get; set; }
        
        [DisplayName("Номенклатура")]
        public string Nomenclature { get; set; }

        [DisplayName("Вологість по документах у відсотках")]
        public double? DocHumidityValue { get; set; }

        [DisplayName("Засміченість по документах у відсотках")]
        public double? DocImpurityValue { get; set; }

        [DisplayName("В'їзд дозволено")]
        public bool IsAllowedToEnterTerritory { get; set; }
    }
}