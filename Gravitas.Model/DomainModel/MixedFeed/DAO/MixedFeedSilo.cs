using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;

namespace Gravitas.Model.DomainModel.MixedFeed.DAO
{
    public class MixedFeedSilo: BaseEntity<int>
    {
        public bool IsActive { get; set; }
        public int Drive { get; set; }
        public int LoadQueue { get; set; }
        public float SiloWeight { get; set; }
        public float SiloEmpty { get; set; }
        public float SiloFull { get; set; }
        public string Specification { get; set; }
        
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}