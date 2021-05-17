using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DTO
{
    public class DeliveryBillStatusDetail : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}