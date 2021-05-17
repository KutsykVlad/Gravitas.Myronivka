using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetCropModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Crops";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetCropValueDto.Response> Items { get; set; }
            }
        }
    }
}