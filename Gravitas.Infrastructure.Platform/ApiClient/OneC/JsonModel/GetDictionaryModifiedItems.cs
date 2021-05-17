using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetDictionaryModifiedItemsDto
        {
            public class Request
            {
                [JsonProperty("Dictionary")]
                public virtual string Dictionary { get; set; }
            }

            public class Response : BaseResponseDto
            {
            }
        }
    }
}