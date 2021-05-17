using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.Node.TDO.List
{
    public class NodeItems
    {
        public ICollection<NodeItem> Items { get; set; }
        public int Count { get; set; }
    }
}