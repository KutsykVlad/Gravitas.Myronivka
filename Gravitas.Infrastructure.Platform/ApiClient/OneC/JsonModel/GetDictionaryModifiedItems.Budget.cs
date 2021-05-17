using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetBudgetModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Budgets";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetBudgetValueDto.Response> Items { get; set; }
            }
        }
    }
}