namespace Gravitas.Model {

	public static partial class ExternalData {

		public class DeliveryBill : BaseEntity<string> {
			// 105
			//public string Id { get; set; }
			// 106
			public string Code { get; set; }
			// 41
			public string SupplyTypeId { get; set; }
			// 39
			public string OrderCode { get; set; }
			// 8
			public string DocumentTypeId { get; set; }
			// 1
			public string OrganizationId { get; set; }
			// 38
			public string SenderOrganisationId { get; set; }
			// 9
			public string SenderDepotId { get; set; }
			// 10
			public string ReceiverTypeId { get; set; }
			// 11
			public string ReceiverId { get; set; }
			// 12
			public string ReceiverAnaliticsId { get; set; }
			// 13
			public string ProductId { get; set; }
			// 14
			public string HarvestId { get; set; }
			// 31
			public string BuyBudgetId { get; set; }
			// 32
			public string SellBudgetsId { get; set; }
		}
	}
}