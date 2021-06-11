using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages.Models
{
    public class OmniMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "single";
        
        [JsonProperty("validity")]
        public string Validity { get; set; } = "+30 min";
        
        [JsonProperty("extended")]
        public bool Extended { get; set; } = true;
        
        [JsonProperty("source")]
        public string Source { get; set; } = "MHP_KE";
        
        [JsonProperty("desc")]
        public string Desc { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; } = "SMS";
        
        [JsonProperty("to")]
        public Receiver[] To { get; set; }
        
        [JsonProperty("body")]
        public Body Body { get; set; }
    }

    public class Receiver
    {
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
    }
    
    public class Body
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}