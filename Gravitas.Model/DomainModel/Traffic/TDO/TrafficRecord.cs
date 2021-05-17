using System;

namespace Gravitas.Model
{
    public class TrafficRecord
    {
        public long CurrentNodeId { get; set; }
        public long TicketContainerId { get; set; }
        public DateTime EntranceTime { get; set; }
    }
}
