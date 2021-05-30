using System;
using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO.Json;
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
                public Guid? SupplyTypeId { get; set; }

                // 39
                [JsonProperty("OrderNumber")]
                public string OrderCode { get; set; }

                // 8
                [JsonProperty("DocumentType")]
                public string DocumentTypeId { get; set; }

                // 1
                [JsonProperty("OrganizationId")]
                public Guid? OrganizationId { get; set; }

                // 38
                [JsonProperty("KeeperOrganizationId")]
                public Guid? KeeperOrganizationId { get; set; }

                // 9
                [JsonProperty("StockID")]
                public Guid? StockId { get; set; }

                // 10
                [JsonProperty("ReceiverType")]
                public Guid? ReceiverTypeId { get; set; }

                // 11
                [JsonProperty("ReceiverId")]
                public Guid? ReceiverId { get; set; }

                // 12
                [JsonProperty("ReceiverAnaliticsId")]
                public Guid? ReceiverAnaliticsId { get; set; }

                // 13
                [JsonProperty("ProductId")]
                public Guid? ProductId { get; set; }

                // 14
                [JsonProperty("HarvestId")]
                public Guid? HarvestId { get; set; }

                // 31
                [JsonProperty("BuyBudgetId")]
                public Guid? BuyBudgetId { get; set; }

                // 32
                [JsonProperty("SellBudgetsId")]
                public Guid? SellBudgetsId { get; set; }

                [JsonProperty("ProductContents")]
                public List<ProductContent> ProductContents { get; set; }
            }
        }
    }
}