using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;

namespace Gravitas.Model.DomainModel.Queue.DAO
{
    public class QueuePatternItem: BaseEntity<int>
    {
		public int Count { get; set; }
        public int CategoryId { get; set; }
		public int PriorityId { get; set; }
		public string PartnerId { get; set; }
        
        public Partner Partner { get; set; }
        public QueueItemPriority QueueItemPriority { get; set; }
        public QueueItemCategory QueueItemCategory { get; set; }
    }
}
