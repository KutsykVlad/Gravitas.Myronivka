using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO
{
    public class DeliveryBillType : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}