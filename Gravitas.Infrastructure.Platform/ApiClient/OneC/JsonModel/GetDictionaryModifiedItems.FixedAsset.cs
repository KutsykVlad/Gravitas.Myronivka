using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetFixedAssetModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "FixedAssets";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetFixedAssetValueDto.Response> Items { get; set; }
            }
        }
    }
}