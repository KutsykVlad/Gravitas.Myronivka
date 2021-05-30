using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.Model.DomainModel.ExternalData.Partner.DAO
{
    public class Partner : BaseEntity<Guid>
    {
        public Partner()
        {
            QueuePatternItemsSet = new List<QueuePatternItem>();
        }

        public ICollection<QueuePatternItem> QueuePatternItemsSet { get; set; }

        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? CarrierDriverId { get; set; }
    }
}