using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetProductValueDto
        {
            public class Request : GetDictionaryValueDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Products";
            }

            public class Response : GetDictionaryValueDto.Response.DictionaryType5
            {
            }
        }
    }
}