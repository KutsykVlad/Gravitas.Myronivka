using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gravitas.Platform.Web.ViewModel
{
    public class NodeTrafficListVm
    {
        public int NodeId { get; set; }
        [DisplayName("Назва вузла")]
        public string NodeName { get; set; }
        [DisplayName("Середній час у хвилинах")]
        public int AverageTime { get; set; }
        [DisplayName("Машин в черзі")]
        public int ElementsInQueue { get; set; }

        public IEnumerable<NodeTrafficItem> Items { get; set; }

        public int Count { get; set; }
    }
}