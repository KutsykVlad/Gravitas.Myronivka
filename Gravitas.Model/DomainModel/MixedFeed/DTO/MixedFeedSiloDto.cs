namespace Gravitas.Model.DomainModel.MixedFeed.DTO
{
    public class MixedFeedSiloDto : BaseEntity<long>
    {
        public bool IsActive { get; set; }
        public int Drive { get; set; }
        public int LoadQueue { get; set; }
        public long SiloWeight { get; set; }
        public float SiloEmpty { get; set; }
        public float SiloFull { get; set; }
    
        public ExternalData.Product Product { get; set; }
    }
}