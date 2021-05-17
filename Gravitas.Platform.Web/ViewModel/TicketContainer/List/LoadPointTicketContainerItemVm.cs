using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class LoadPointTicketContainerItemVm
    {
        [DisplayName("Номенклатура")]
        public string ProductName { get; set; }

        [DisplayName("Погрузити, кг.")]
        public double LoadTarget { get; set; }

        [DisplayName("Плюс, кг.")]
        public int LoadTargetDeviationPlus { get; set; }

        [DisplayName("Мінус, кг.")]
        public int LoadTargetDeviationMinus { get; set; }

        [DisplayName("Транспорт")]
        public string TransportNo { get; set; }

        [DisplayName("Отримувач")]
        public string ReceiverName { get; set; }

        [DisplayName("Склад контрагента")]
        public string ReceiverDepotName { get; set; }
    }
}