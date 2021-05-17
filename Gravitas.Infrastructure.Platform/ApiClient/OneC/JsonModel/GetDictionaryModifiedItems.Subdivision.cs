using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetSubdivisionModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Subdivisions";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetSubdivisionValueDto.Response> Items { get; set; }
            }
        }
    }
}