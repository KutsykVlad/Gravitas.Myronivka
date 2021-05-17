using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class QueueItemCategory : BaseEntity<int>
    {
        public string Description { get; set; }

        public QueueItemCategory()
        {
            QueuePatternItemsSet = new List<QueuePatternItem>();
        }

        public ICollection<QueuePatternItem> QueuePatternItemsSet { get; set; }
    }
}
