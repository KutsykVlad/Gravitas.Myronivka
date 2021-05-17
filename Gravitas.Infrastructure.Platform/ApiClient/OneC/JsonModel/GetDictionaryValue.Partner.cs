using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetPartnerValueDto
        {
            public class Request : GetDictionaryValueDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Partners";
            }

            public class Response : GetDictionaryValueDto.Response.DictionaryType1
            {
            }
        }
    }
}