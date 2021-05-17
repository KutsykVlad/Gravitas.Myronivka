using System;
using Newtonsoft.Json;

namespace Gravitas.DAL.DeviceTransfer
{
    public class DeviceStateTransfer
    {
        [JsonProperty("DeviceType")]
        public long DeviceType { get; set; }
        [JsonProperty("LastUpdate")]
        public DateTime? LastUpdate { get; set; }
        [JsonProperty("ErrorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("InData")]
        public string InData { get; set; }
        [JsonProperty("OutData")]
        public string OutData { get; set; }
        [JsonProperty("Id")]
        public long Id { get; set; }
    }
}