using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetReasonForRefundValueDto
        {
            public class Request : GetDictionaryValueDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "ReasonsForRefunds";
            }

            public class Response : GetDictionaryValueDto.Response.DictionaryType4
            {
            }
        }
    }
}