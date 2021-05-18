using System;

namespace Gravitas.Model.DomainModel.Traffic.TDO
{
    public class TrafficRecord
    {
        public int CurrentNodeId { get; set; }
        public int TicketContainerId { get; set; }
        public DateTime EntranceTime { get; set; }
    }
}
