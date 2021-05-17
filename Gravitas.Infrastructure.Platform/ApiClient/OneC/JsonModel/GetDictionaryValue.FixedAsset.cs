using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetFixedAssetValueDto
        {
            public class Request : GetDictionaryValueDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "FixedAssets";
            }

            public class Response : GetDictionaryValueDto.Response.DictionaryType6
            {
            }
        }
    }
}