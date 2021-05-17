using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetRouteModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Routes";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetRouteValueDto.Response> Items { get; set; }
            }
        }
    }
}