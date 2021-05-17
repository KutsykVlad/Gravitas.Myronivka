using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO
{
    public class DeliveryBillStatus : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}