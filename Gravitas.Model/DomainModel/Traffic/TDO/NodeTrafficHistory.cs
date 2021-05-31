using System.Collections.Generic;
using Gravitas.Model.DomainModel.Traffic.DAO;

namespace Gravitas.Model
{
    public class NodeTrafficHistory
    {
        public NodeTrafficHistory()
        {
            Items = new List<TrafficHistory>();
        }

        public int NodeId { get; set; }

        public ICollection<TrafficHistory> Items { get; set; }

        public int Count { get; set; }
    }
}
