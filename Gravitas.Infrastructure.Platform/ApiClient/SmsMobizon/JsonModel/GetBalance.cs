using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public partial class SmsMobizonApiClient
    {
        public static class GetBalanceDto
        {
            public class Response : BaseResponseDto
            {
                [JsonProperty("data")]
                public Data DataArray { get; set; }

                public class Data
                {
                    [JsonProperty("balance")]
                    public double Balance { get; set; }

                    [JsonProperty("currency")]
                    public string Currency { get; set; }
                }
            }
        }
    }
}