using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Queue.DAO
{
    public class QueuePatternItem: BaseEntity<int>
    {
		public int Count { get; set; }
        public QueueCategory CategoryId { get; set; }
		public QueuePriority PriorityId { get; set; }
		public string PartnerId { get; set; }
        
        public Partner Partner { get; set; }
        public QueueItemPriority QueueItemPriority { get; set; }
        public QueueItemCategory QueueItemCategory { get; set; }
    }
}
