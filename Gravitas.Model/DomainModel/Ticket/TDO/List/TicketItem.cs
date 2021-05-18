using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.Dto {
	
	public class TicketItem : BaseEntity<int> {

		public TicketStatus StatusId { get; set; }
		public string StatusName { get; set; }
		public string SupplyCode { get; set; }
		public string Product { get; set; }
	}
}
