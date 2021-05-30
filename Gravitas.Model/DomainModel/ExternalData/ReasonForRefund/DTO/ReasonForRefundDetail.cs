using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DTO
{
    public class ReasonForRefundDetail : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}