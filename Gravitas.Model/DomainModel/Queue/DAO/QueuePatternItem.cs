namespace Gravitas.Model
{
    public class QueuePatternItem: BaseEntity<long>
    {
		public int Count { get; set; }
   
        public long CategoryId { get; set; }
     
		public long PriorityId { get; set; }
             
		public string PartnerId { get; set; }
        
        public ExternalData.Partner Partner { get; set; }
        public QueueItemPriority QueueItemPriority { get; set; }
        public QueueItemCategory QueueItemCategory { get; set; }
    }
}
