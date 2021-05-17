using System.ComponentModel;
using Gravitas.Platform.Web.Manager;

namespace Gravitas.Platform.Web.ViewModel
{
    public class MixedFeedUnloadTicketContainerItemVm
    {
//        [DisplayName("Номенклатура")]
//        public string ProductName { get; set; }
//        
//        [DisplayName("Розвантажити, кг.")]
//        public double LoadTarget { get; set; }
//
//        [DisplayName("Плюс, кг.")]
//        public int LoadTargetDeviationPlus { get; set; }
//
//        [DisplayName("Мінус, кг.")]
//        public int LoadTargetDeviationMinus { get; set; }
//
//        [DisplayName("Транспорт")]
//        public string TransportNo { get; set; }
//
//        [DisplayName("Отримувач")]
//        public string ReceiverName { get; set; }
//        
//        [DisplayName("Склад контрагента")]
//        public string ReceiverDepotName { get; set; }
        
        [DisplayName("Ост. опрацьована точка")]
        public string LastNodeName { get; set; }
        
        public BaseRegistryData BaseData { get; set; }
        
        [DisplayName("Розвантажити, кг.")]
        public double LoadTarget { get; set; }
    }
}