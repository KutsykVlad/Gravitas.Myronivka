using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpDataEvent.DTO;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class ChangeSupplyCodeDto
        {
            public class Request
            {
                [JsonProperty("Id")]
                public string Id { get; set; }

                [JsonProperty("NewSupplyCode")]
                public string NewSupplyCode { get; set; }

                [JsonProperty("Activity")]
                public int Activity { get; set; }

                [JsonProperty("WeightingStatistics")]
                public OpDataEventDto[] WeightingStatistics { get; set; }
            }

            public class Response : BaseResponseDto
            {
                // 105
                [JsonProperty("Id")]
                public string Id { get; set; }

                // 106
                [JsonProperty("Number")]
                public string Code { get; set; }

                // 41
                [JsonProperty("SupplyType")]
                public string SupplyTypeId { get; set; }

                // 39
                [JsonProperty("OrderNumber")]
                public string OrderCode { get; set; }

                // 8
                [JsonProperty("DocumentType")]
                public string DocumentTypeId { get; set; }

                // 1
                [JsonProperty("OrganizationId")]
                public string OrganizationId { get; set; }

                // 38
                [JsonProperty("KeeperOrganizationId")]
                public string KeeperOrganizationId { get; set; }

                // 9
                [JsonProperty("StockID")]
                public string StockId { get; set; }

                // 10
                [JsonProperty("ReceiverType")]
                public string ReceiverTypeId { get; set; }

                // 11
                [JsonProperty("ReceiverId")]
                public string ReceiverId { get; set; }

                // 12
                [JsonProperty("ReceiverAnaliticsId")]
                public string ReceiverAnaliticsId { get; set; }

                // 13
                [JsonProperty("ProductId")]
                public string ProductId { get; set; }

                // 14
                [JsonProperty("HarvestId")]
                public string HarvestId { get; set; }

                // 31
                [JsonProperty("BuyBudgetId")]
                public string BuyBudgetId { get; set; }

                // 32
                [JsonProperty("SellBudgetsId")]
                public string SellBudgetsId { get; set; }

                [JsonProperty("ProductContents")]
                public List<ProductContent> ProductContents { get; set; }
            }
        }
    }
}