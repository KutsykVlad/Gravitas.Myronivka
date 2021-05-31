using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;

namespace Gravitas.Model.DomainModel.MixedFeed.DTO
{
    public class MixedFeedSiloDto : BaseEntity<int>
    {
        public bool IsActive { get; set; }
        public int Drive { get; set; }
        public int LoadQueue { get; set; }
        public int SiloWeight { get; set; }
        public float SiloEmpty { get; set; }
        public float SiloFull { get; set; }
    
        public Product Product { get; set; }
    }
}