using System;

namespace Gravitas.Model.DomainModel.Node.TDO.Json
{
    public class NodeContext
    {
        public int? OpRoutineStateId { get; set; }
        public int? TicketContainerId { get; set; }
        public int? TicketId { get; set; }
        public Guid? OpDataId { get; set; }
        public int? OpDataComponentId { get; set; }
        public int? OpProcessData { get; set; }
        public DateTime? LastStateChangeTime { get; set; }
    }
}