using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetUnitModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Units";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetUnitValueDto.Response> Items { get; set; }
            }
        }
    }
}