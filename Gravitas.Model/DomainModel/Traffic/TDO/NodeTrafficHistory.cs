using System.Collections.Generic;

namespace Gravitas.Model
{
    public class NodeTrafficHistory
    {
        public NodeTrafficHistory()
        {
            Items = new List<TrafficHistory>();
        }

        public long NodeId { get; set; }

        public ICollection<TrafficHistory> Items { get; set; }

        public int Count { get; set; }
    }
}
