using System;
using System.Collections.Generic;
using Gravitas.Model;
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
                public string CarrierCodeId { get; set; }

                [JsonProperty("CarrierRouteId")]
                public string CarrierRouteId { get; set; }

                [JsonProperty("ContractCarrierId")]
                public string ContractCarrierId { get; set; }

                [JsonProperty("TransportId")]
                public string TransportId { get; set; }

                [JsonProperty("TrailerId")]
                public string TrailerId { get; set; }

                [JsonProperty("DriverOneId")]
                public string DriverOneId { get; set; }

                [JsonProperty("DriverTwoId")]
                public string DriverTwoId { get; set; }

                [JsonProperty("HiredTansportNumberId")]
                public string HiredTransportNumberId { get; set; }

                [JsonProperty("HiredTrailerNumberId")]
                public string HiredTrailerNumberId { get; set; }

                [JsonProperty("IncomeDocDate")]
                public DateTime? IncomeDocDate { get; set; }

                [JsonProperty("Number")]
                public string Code { get; set; }

                [JsonProperty("SupplyType")]
                public string SupplyTypeId { get; set; }

                [JsonProperty("SupplyCode")]
                public string SupplyCode { get; set; }

                [JsonProperty("OrderNumber")]
                public string OrderCode { get; set; }

                [JsonProperty("DocumentType")]
                public string DocumentTypeId { get; set; }

                [JsonProperty("OrganizationId")]
                public string OrganizationId { get; set; }

                [JsonProperty("KeeperOrganizationId")]
                public string KeeperOrganizationId { get; set; }

                [JsonProperty("StockID")]
                public string StockId { get; set; }

                [JsonProperty("ReceiverType")]
                public string ReceiverTypeId { get; set; }

                [JsonProperty("ReceiverId")]
                public string ReceiverId { get; set; }

                [JsonProperty("ReceiverStockId")]
                public string ReceiverDepotId { get; set; }

                [JsonProperty("ReceiverAnaliticsId")]
                public string ReceiverAnaliticsId { get; set; }

                [JsonProperty("ProductId")]
                public string ProductId { get; set; }

                [JsonProperty("HarvestId")]
                public string HarvestId { get; set; }

                [JsonProperty("BuyBudgetId")]
                public string BuyBudgetId { get; set; }

                [JsonProperty("SellBudgetId")]
                public string SellBudgetId { get; set; }

                [JsonProperty("StatusType")]
                public string StatusType { get; set; }

                [JsonProperty("ProductContents")]
                public List<ProductContent> ProductContents { get; set; }
            }
        }
    }
}