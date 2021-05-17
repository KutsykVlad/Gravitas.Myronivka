using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {
	
	public class TicketItem : BaseEntity<int> {

		public long StatusId { get; set; }
		public string StatusName { get; set; }
		public string SupplyCode { get; set; }
		public string Product { get; set; }
	}
}
