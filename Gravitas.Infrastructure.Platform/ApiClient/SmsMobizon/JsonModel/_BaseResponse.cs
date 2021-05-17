using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public partial class SmsMobizonApiClient
    {
        public class BaseResponseDto
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}