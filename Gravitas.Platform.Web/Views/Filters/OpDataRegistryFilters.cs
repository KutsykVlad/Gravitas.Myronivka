using System;
using System.Collections.Generic;

namespace Gravitas.Platform.Web.Models
{
    public class OpDataRegistryFilters
    {
        public int? TicketContainerId { get; set; }
        public int? TicketId { get; set; }
        public IEnumerable<long> NodeIds { get; set; }
        public int? NodeType { get; set; }
        public int? OpDataType { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public int? StateId { get; set; }
    }
}