using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO
{
    public class DeliveryBill : BaseEntity<string>
    {
        public string Code { get; set; }
        public string SupplyTypeId { get; set; }
        public string OrderCode { get; set; }
        public string DocumentTypeId { get; set; }
        public string OrganizationId { get; set; }
        public string SenderOrganisationId { get; set; }
        public string SenderDepotId { get; set; }
        public string ReceiverTypeId { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverAnaliticsId { get; set; }
        public string ProductId { get; set; }
        public string HarvestId { get; set; }
        public string BuyBudgetId { get; set; }
        public string SellBudgetsId { get; set; }
    }
}