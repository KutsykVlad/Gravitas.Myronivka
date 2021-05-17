using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetAcceptancePointModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "AcceptancePoints";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetAcceptancePointValueDto.Response> Items { get; set; }
            }
        }
    }
}