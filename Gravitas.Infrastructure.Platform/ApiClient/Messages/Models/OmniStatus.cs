using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages.Models
{
    public class OmniStatus
    {
        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public class Request
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}