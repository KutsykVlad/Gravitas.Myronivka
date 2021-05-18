using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainModel.Traffic.DAO;
using TicketContainerStatus = Gravitas.Model.DomainValue.TicketContainerStatus;

namespace Gravitas.Model
{
    [Table("TicketContainer")]
    public class TicketContainer : BaseEntity<int>
    {
        public TicketContainer()
        {
            CardSet = new HashSet<Card>();
            TicketSet = new HashSet<Ticket>();
            TrafficHistory = new List<TrafficHistory>();
        }

        public int StatusId { get; set; }
        public int QueueStatusId { get; set; }
        public string ProcessingMessage { get; set; }

        public virtual Gravitas.Model.DomainModel.Ticket.DAO.TicketContainerStatus TicketContainerStatus { get; set; }
        public virtual ICollection<Card> CardSet { get; set; }
        public virtual ICollection<Ticket> TicketSet { get; set; }
        public virtual ICollection<TrafficHistory> TrafficHistory { get; set; }
    }
}