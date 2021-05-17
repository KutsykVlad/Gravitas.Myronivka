using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetUserModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Users";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetUserValueDto.Response> Items { get; set; }
            }
        }
    }
}