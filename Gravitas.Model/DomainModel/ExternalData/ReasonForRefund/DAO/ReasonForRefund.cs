using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DAO
{
    public class ReasonForRefund : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}