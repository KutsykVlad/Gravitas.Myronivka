using System;

namespace Gravitas.Model
{
    public class TrafficHistory: BaseEntity<long>
    {
        public long TicketContainerId { get; set; }
        public long NodeId { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? DepartureTime { get; set; }

        public virtual TicketContainer TicketContainer { get; set; }
        public virtual Node Node { get; set; }
    }
}
