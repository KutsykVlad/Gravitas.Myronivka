using System.Collections.Generic;
using System.Linq;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class RouteInfo
    {
        public int TicketContainerId { get; set; }
        public int CurrentNode { get; set; }

        //For SMS
        public int ActiveTicketId { get; set; }

        public List<List<int>> PathNodes => GroupAlternativeNodes.Select(s => s.Nodes).ToList();
        internal List<GroupAlternativeNodes> GroupAlternativeNodes { get; set; }
        public List<int> TicketIds { get; set; } = new List<int>();
    }

    public class GroupAlternativeNodes
    {
        public List<int> Nodes { get; set; }
        public bool QuotaEnabled { get; set; }
    }
}