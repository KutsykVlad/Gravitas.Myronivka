using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public class BaseResponseDto
        {
            [JsonProperty("ErrorCode")]
            public int ErrorCode { get; set; }

            [JsonProperty("ErrorMsg")]
            public string ErrorMsg { get; set; }
        }
    }
}