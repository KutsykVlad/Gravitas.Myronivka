using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.DAO;

namespace Gravitas.Model
{
    public class TrafficHistory: BaseEntity<int>
    {
        public long TicketContainerId { get; set; }
        public long NodeId { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? DepartureTime { get; set; }

        public virtual TicketContainer TicketContainer { get; set; }
        public virtual Node Node { get; set; }
    }
}
