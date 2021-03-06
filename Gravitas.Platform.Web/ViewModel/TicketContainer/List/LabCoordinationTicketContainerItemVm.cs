using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel {
    
    public class LabCoordinationTicketContainerItemVm {
        
        [DisplayName("Id")]
        public long TicketContainerId { get; set; }
        [DisplayName("Номер картки водія")]
        public string CardNumber { get; set; }
        [DisplayName("Транспорт No.")]
        public string TransportNo { get; set; }
        [DisplayName("Причіп No.")]
        public string TrailerNo { get; set; }
        [DisplayName("Номенклатура")]
        public string ProductName { get; set; }
        [DisplayName("Відправник")]
        public string SenderName { get; set; }
        [DisplayName("Власний перевізник?")]
        public bool IsThirdPartyCarrier { get; set; }

        [DisplayName("Засміченість")]
        public double? ImpurityValue { get; set; }
        [DisplayName("Вологість")]
        public double? HumidityValue { get; set; }

        [DisplayName("Коментар")]
        public string Comment { get; set; }
    }
}