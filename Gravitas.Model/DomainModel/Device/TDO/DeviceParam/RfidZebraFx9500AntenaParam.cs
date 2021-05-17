using Newtonsoft.Json;

namespace Gravitas.Model.DomainModel.Device.TDO.DeviceParam
{
    public class RfidZebraFx9500AntennaParam
    {
        [JsonProperty("AntennaId")] 
        public int AntennaId { get; set; }
        
        [JsonProperty("PeakRssiLimit")] 
        public int PeakRssiLimit { get; set; }
        
        [JsonProperty("TagActiveSeconds")] 
        public int TagActiveSeconds { get; set; }
    }
}