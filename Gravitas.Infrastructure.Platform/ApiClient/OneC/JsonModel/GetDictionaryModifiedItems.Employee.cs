using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetEmployeeModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Employees";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetEmployeeValueDto.Response> Items { get; set; }
            }
        }
    }
}