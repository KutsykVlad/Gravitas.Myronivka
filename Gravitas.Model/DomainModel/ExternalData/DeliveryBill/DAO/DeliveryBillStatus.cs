using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO
{
    public class DeliveryBillStatus : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}