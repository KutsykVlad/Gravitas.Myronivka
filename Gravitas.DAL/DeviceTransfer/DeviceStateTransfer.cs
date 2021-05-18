using System;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;

namespace Gravitas.DAL.DeviceTransfer
{
    public class DeviceStateTransfer
    {
        [JsonProperty("DeviceType")]
        public DeviceType DeviceType { get; set; }
        [JsonProperty("LastUpdate")]
        public DateTime? LastUpdate { get; set; }
        [JsonProperty("ErrorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("InData")]
        public string InData { get; set; }
        [JsonProperty("OutData")]
        public string OutData { get; set; }
        [JsonProperty("Id")]
        public int Id { get; set; }
    }
}