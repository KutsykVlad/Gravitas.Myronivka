using System.Collections.Generic;

namespace Gravitas.Model
{
    public class QueueItemPriority : BaseEntity<long>
    {
        public string Description { get; set; }

        public QueueItemPriority()
        {
            Items = new List<QueuePatternItem>();
        }

        public ICollection<QueuePatternItem> Items { get; set; }
    }
}
