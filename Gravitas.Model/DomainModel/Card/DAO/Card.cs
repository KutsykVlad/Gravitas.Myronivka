namespace Gravitas.Model {

	public class Card : BaseEntity<string> {
		
		public long TypeId { get; set; }
		public int No { get; set; }
		public bool IsActive { get; set; }
		public bool IsOwn { get; set; }
		public string EmployeeId { get; set; }
		public long? TicketContainerId { get; set; }
		public string ParentCardId { get; set; } 

		// Navigation properties
		public virtual ExternalData.Employee Employee { get; set; }
		public virtual TicketContainer TicketConteiner { get; set; }
		public virtual CardType CardType { get; set; }
	}
}
