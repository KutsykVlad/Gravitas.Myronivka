using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetYearOfHarvestModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "YearsOfHarvest";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetYearOfHarvestValueDto.Response> Items { get; set; }
            }
        }
    }
}