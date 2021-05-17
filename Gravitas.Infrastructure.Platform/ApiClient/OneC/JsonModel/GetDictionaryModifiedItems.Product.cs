using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetProductModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Products";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetProductValueDto.Response> Items { get; set; }
            }
        }
    }
}