using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel {

    public class UnloadPointTicketContainerItemVm {

	    [DisplayName("Id")]
	    public long TicketContainerId { get; set; }
	    [DisplayName("Картка")]
	    public string CardNumber { get; set; }
        [DisplayName("№ ТТН")]
        public string DelliveryBillCode { get; set; }
        [DisplayName("Транспорт")]
	    public string TransportNo { get; set; }
	    [DisplayName("Причеп")]
	    public string TrailerNo { get; set; }
		[DisplayName("Відправник")]
        public string SenderName { get; set; }
		[DisplayName("Перевізник")]
        public bool IsThirdPartyCarrier { get; set; }
	    [DisplayName("Коментар ТТН")]
	    public string Comment { get; set; }
	    [DisplayName("Вага")]
	    public double WeightValue { get; set; }
		[DisplayName("Номенклатура")]
	    public string Product { get; set; }
    }
}