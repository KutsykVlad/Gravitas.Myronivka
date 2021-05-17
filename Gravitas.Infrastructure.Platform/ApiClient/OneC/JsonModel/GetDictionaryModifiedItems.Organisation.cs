using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetOrganisationModifiedItemsDto
        {
            public class Request : GetDictionaryModifiedItemsDto.Request
            {
                [JsonProperty("Dictionary")]
                public override string Dictionary => "Organizations";
            }

            public class Response : GetDictionaryModifiedItemsDto.Response
            {
                public List<GetOrganisationValueDto.Response> Items { get; set; }
            }
        }
    }
}