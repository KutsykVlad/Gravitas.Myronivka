using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetYearOfHarvestValueDto
        {
            public class Request : GetDictionaryValueDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "YearsOfHarvest";
            }

            public class Response : GetDictionaryValueDto.Response.DictionaryType4
            {
            }
        }
    }
}