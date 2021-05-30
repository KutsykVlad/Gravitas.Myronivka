using System;
using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO.Json;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetDeliveryBill
        {
            public class BarCoreRequest
            {
                [JsonProperty("BarCode")]
                public string BarCode { get; set; }
            }

            public class SupplyCodeRequest
            {
                [JsonProperty("SupplyTypeCode")]
                public string SupplyTypeCode { get; set; }
            }

            public class Response : BaseResponseDto
            {
                [JsonProperty("Id")]
                public string Id { get; set; }

                [JsonProperty("OriginalТТN")]
                public string OriginalТТN { get; set; }

                [JsonProperty("IsThirdPartyCarrier")]
                public string IsThirdPartyCarrier { get; set; }

                [JsonProperty("SenderWeight")]
                public double? SenderWeight { get; set; }

                [JsonProperty("HiredDriverCode")]
                public string HiredDriverCode { get; set; }

                [JsonProperty("IncomeInvoiceSeries")]
                public string IncomeInvoiceSeries { get; set; }

                [JsonProperty("IncomeInvoiceNumber")]
                public string IncomeInvoiceNumber { get; set; }

                [JsonProperty("IncomeDocGrossValue")]
                public double? IncomeDocGrossValue { get; set; }

                [JsonProperty("IncomeDocTareValue")]
                public double? IncomeDocTareValue { get; set; }

                [JsonProperty("CarrierCodeId")]
                public Guid? CarrierCodeId { get; set; }

                [JsonProperty("CarrierRouteId")]
                public string CarrierRouteId { get; set; }

                [JsonProperty("ContractCarrierId")]
                public Guid? ContractCarrierId { get; set; }

                [JsonProperty("TransportId")]
                public Guid? TransportId { get; set; }

                [JsonProperty("TrailerId")]
                public Guid? TrailerId { get; set; }

                [JsonProperty("DriverOneId")]
                public Guid? DriverOneId { get; set; }

                [JsonProperty("DriverTwoId")]
                public Guid? DriverTwoId { get; set; }

                [JsonProperty("HiredTansportNumberId")]
                public string HiredTransportNumberId { get; set; }

                [JsonProperty("HiredTrailerNumberId")]
                public string HiredTrailerNumberId { get; set; }

                [JsonProperty("IncomeDocDate")]
                public DateTime? IncomeDocDate { get; set; }

                [JsonProperty("Number")]
                public string Code { get; set; }

                [JsonProperty("SupplyType")]
                public Guid? SupplyTypeId { get; set; }

                [JsonProperty("SupplyCode")]
                public string SupplyCode { get; set; }

                [JsonProperty("OrderNumber")]
                public string OrderCode { get; set; }

                [JsonProperty("DocumentType")]
                public string DocumentTypeId { get; set; }

                [JsonProperty("OrganizationId")]
                public Guid? OrganizationId { get; set; }

                [JsonProperty("KeeperOrganizationId")]
                public Guid? KeeperOrganizationId { get; set; }

                [JsonProperty("StockID")]
                public Guid? StockId { get; set; }

                [JsonProperty("ReceiverType")]
                public Guid? ReceiverTypeId { get; set; }

                [JsonProperty("ReceiverId")]
                public Guid? ReceiverId { get; set; }

                [JsonProperty("ReceiverStockId")]
                public Guid? ReceiverDepotId { get; set; }

                [JsonProperty("ReceiverAnaliticsId")]
                public Guid? ReceiverAnaliticsId { get; set; }

                [JsonProperty("ProductId")]
                public Guid? ProductId { get; set; }

                [JsonProperty("HarvestId")]
                public Guid? HarvestId { get; set; }

                [JsonProperty("BuyBudgetId")]
                public Guid? BuyBudgetId { get; set; }

                [JsonProperty("SellBudgetId")]
                public Guid? SellBudgetId { get; set; }

                [JsonProperty("StatusType")]
                public string StatusType { get; set; }

                [JsonProperty("ProductContents")]
                public List<ProductContent> ProductContents { get; set; }
            }
        }
    }
}