using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.Model.DomainModel.ExternalData.Product.DAO
{
    public class Product : BaseEntity<Guid>
    {
        public Product()
        {
            MixedFeedSiloSet = new HashSet<MixedFeedSilo>();
        }

        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }

        public virtual ICollection<MixedFeedSilo> MixedFeedSiloSet { get; set; }
    }
}