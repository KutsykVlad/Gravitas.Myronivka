using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DTO
{
    public class DeliveryBillTypeDetail : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}