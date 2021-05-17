using System;
using System.Collections.Generic;

namespace Gravitas.Platform.Web.Models
{
    public class OpDataRegistryFilters
    {
        public long? TicketContainerId { get; set; }
        public long? TicketId { get; set; }
        public IEnumerable<long> NodeIds { get; set; }
        public long? NodeType { get; set; }
        public long? OpDataType { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public long? StateId { get; set; }
    }
}