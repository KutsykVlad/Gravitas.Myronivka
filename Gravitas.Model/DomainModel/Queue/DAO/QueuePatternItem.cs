using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class QueuePatternItem: BaseEntity<int>
    {
		public int Count { get; set; }
   
        public long CategoryId { get; set; }
     
		public long PriorityId { get; set; }
             
		public string PartnerId { get; set; }
        
        public DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData.Partner Partner { get; set; }
        public QueueItemPriority QueueItemPriority { get; set; }
        public QueueItemCategory QueueItemCategory { get; set; }
    }
}
