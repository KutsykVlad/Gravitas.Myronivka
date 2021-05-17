using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel {
	
	public class TicketItemVm : BaseEntityVm<long> {

		[DisplayName("ID")]
		public long StatusId { get; set; }
		[DisplayName("Статус")]
		public string StatusName { get; set; }
		[DisplayName("Код поставки")]
		public string SupplyCode { get; set; }
		[DisplayName("Номенклатура")]
		public string Product { get; set; }
	}
}
