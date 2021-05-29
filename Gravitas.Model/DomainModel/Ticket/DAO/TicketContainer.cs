using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Traffic.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    [Table("TicketContainer")]
    public class TicketContainer : BaseEntity<int>
    {
        public TicketContainer()
        {
            CardSet = new HashSet<Card.DAO.Card>();
            TicketSet = new HashSet<Ticket>();
            TrafficHistory = new List<TrafficHistory>();
        }

        public TicketContainerStatus StatusId { get; set; }

        public virtual ICollection<Card.DAO.Card> CardSet { get; set; }
        public virtual ICollection<Ticket> TicketSet { get; set; }
        public virtual ICollection<TrafficHistory> TrafficHistory { get; set; }
    }
}