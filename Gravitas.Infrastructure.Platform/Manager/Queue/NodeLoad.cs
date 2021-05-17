using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class NodeLoad
    {
        public List<NodeRouteLoad> RoutesLoad = new List<NodeRouteLoad>();
        public int MaxQuata { get; set; }

        public double GetFutureLoad(int nodesBeforeArrival)
        {
            return RoutesLoad.Count(r => r.NodesBeforeArrival == Math.Max(0, nodesBeforeArrival));
        }

        public string NodeLoadToString(int nodesBeforeArrival)
        {
            var res = new StringBuilder();
            RoutesLoad
                .Where(r => r.NodesBeforeArrival == nodesBeforeArrival)
                .Select(s => s.TicketContainerId)
                .ToList()
                .ForEach(t => res.Append(t).Append(" "));
            return res.ToString();
        }
    }
}