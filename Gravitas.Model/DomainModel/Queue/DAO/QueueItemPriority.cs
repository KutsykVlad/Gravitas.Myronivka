using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class QueueItemPriority : BaseEntity<int>
    {
        public string Description { get; set; }

        public QueueItemPriority()
        {
            Items = new List<QueuePatternItem>();
        }

        public ICollection<QueuePatternItem> Items { get; set; }
    }
}
