using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.Model.DomainModel.Traffic.DAO
{
    public class TrafficHistory : BaseEntity<int>
    {
        public int TicketContainerId { get; set; }
        public int NodeId { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? DepartureTime { get; set; }

        public virtual TicketContainer TicketContainer { get; set; }
        public virtual Node.DAO.Node Node { get; set; }
    }
}