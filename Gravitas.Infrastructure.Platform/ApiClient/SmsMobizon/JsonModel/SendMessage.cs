using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public partial class SmsMobizonApiClient
    {
        public static class SendMessageDto
        {
            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class Request
            {
                [JsonProperty("recipient")]
                public string Recipient { get; set; }

                [JsonProperty("text")]
                public string Text { get; set; }

                [JsonProperty("from")]
                public string From { get; set; }

                [JsonProperty("params")]
                public List<Param> Params { get; set; }

                public class Param
                {
                    [JsonProperty("name")]
                    public string Name { get; set; }

                    [JsonProperty("deferredToTs")]
                    public string DeferredToTs { get; set; }

                    [JsonProperty("mclass")]
                    public int MsgClass { get; set; }

                    [JsonProperty("validity")]
                    public int Validity { get; set; }
                }
            }

            public class Response : BaseResponseDto
            {
                [JsonProperty("data")]
                public Data DataItem { get; set; }

                public static class CampaignStatusCode
                {
                    public const int OnModeration = 1;
                    public const int SentWithoutModeration = 2;
                }

                public class Data
                {
                    [JsonProperty("campaignId")]
                    public int CampaignId { get; set; }

                    [JsonProperty("messageId")]
                    public int MessageId { get; set; }

                    [JsonProperty("status")]
                    public int Status { get; set; }
                }
            }
        }
    }
}