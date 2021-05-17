using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetContractModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Contracts";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetContractValueDto.Response> Items { get; set; }
            }
        }
    }
}