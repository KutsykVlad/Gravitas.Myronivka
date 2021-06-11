using Gravitas.Model.DomainValue;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages.Models
{
    public class OmniSmsSendResult
    {
        [JsonProperty("state")]
        public State Request { get; set; }
        
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class State
    {
        [JsonProperty("value")]
        public MessageStatus Value { get; set; }
    }
}