using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetReasonForRefundModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "ReasonsForRefunds";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetReasonForRefundValueDto.Response> Items { get; set; }
            }
        }
    }
}