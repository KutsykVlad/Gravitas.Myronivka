using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.Queue
{
    public class FilteredExternalQueueItemVm
    {
        [DisplayName("ID контейнеру")]
        public int? TicketContainerId { get; set; }

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