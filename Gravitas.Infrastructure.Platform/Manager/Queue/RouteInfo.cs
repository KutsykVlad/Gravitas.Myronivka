using System.Collections.Generic;
using System.Linq;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class RouteInfo
    {
        public long TicketContainerId { get; set; }
        public long CurrentNode { get; set; }

        //For SMS
        public long ActiveTicketId { get; set; }

        public List<List<long>> PathNodes => GroupAlternativeNodes.Select(s => s.Nodes).ToList();
        internal List<GroupAlternativeNodes> GroupAlternativeNodes { get; set; }
        public List<long> TicketIds { get; set; } = new List<long>();
    }

    public class GroupAlternativeNodes
    {
        public List<long> Nodes { get; set; }
        public bool QuotaEnabled { get; set; }
    }
}