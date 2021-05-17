using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetPartnerModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Partners";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetPartnerValueDto.Response> Items { get; set; }
            }
        }
    }
}