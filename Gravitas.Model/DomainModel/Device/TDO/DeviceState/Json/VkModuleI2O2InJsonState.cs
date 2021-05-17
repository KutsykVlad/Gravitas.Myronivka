using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json
{
    public class VkModuleI2O2InJsonState
    {
        public IDictionary<int, DigitalInJsonState> DigitalIn { get; set; }
    }
}