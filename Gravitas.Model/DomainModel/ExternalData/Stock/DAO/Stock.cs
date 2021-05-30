using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Stock.DAO
{
    public class Stock : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}