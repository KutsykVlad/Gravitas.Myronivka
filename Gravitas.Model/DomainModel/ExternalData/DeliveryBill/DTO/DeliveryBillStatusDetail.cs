using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DTO
{
    public class DeliveryBillStatusDetail : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}