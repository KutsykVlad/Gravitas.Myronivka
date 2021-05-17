using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetStockModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Stocks";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetStockValueDto.Response> Items { get; set; }
            }
        }
    }
}