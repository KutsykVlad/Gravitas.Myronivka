using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Card.DAO;

namespace Gravitas.Model {
	
	[Table("acc.TicketConteiner")]
	public partial class TicketContainer : BaseEntity<int> {

		public TicketContainer() {
			CardSet = new HashSet<Card>();
			TicketSet = new HashSet<Ticket>();
            TrafficHistory = new List<TrafficHistory>();
		}

		public long StatusId { get; set; }
		public long QueueStatusId { get; set; }
		public string ProcessingMessage { get; set; }
		
		// Navigation Properties
		public virtual TicketContainerStatus TicketContainerStatus { get; set; }

		public virtual ICollection<Card> CardSet { get; set; }
		public virtual ICollection<Ticket> TicketSet { get; set; }
        public virtual ICollection<TrafficHistory> TrafficHistory { get; set; }
		
	}
}
