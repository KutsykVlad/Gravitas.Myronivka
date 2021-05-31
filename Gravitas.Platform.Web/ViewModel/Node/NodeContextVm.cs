using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public class NodeContextVm
    {
        public int? OpRoutineStateId { get; set; }
        public int? TicketContainerId { get; set; }
        public int? TicketId { get; set; }
        public Guid? OpDataId { get; set; }
        public int? OpDataComponentId { get; set; }

        public DateTime? LastStateChangeTime { get; set; }
    }
}