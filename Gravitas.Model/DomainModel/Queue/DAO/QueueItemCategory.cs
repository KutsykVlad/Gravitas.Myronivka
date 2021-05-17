using System.Collections.Generic;

namespace Gravitas.Model
{
    public class QueueItemCategory : BaseEntity<long>
    {
        public string Description { get; set; }

        public QueueItemCategory()
        {
            QueuePatternItemsSet = new List<QueuePatternItem>();
        }

        public ICollection<QueuePatternItem> QueuePatternItemsSet { get; set; }
    }
}
