using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json
{
    public class VkModuleI2O2OutJsonState
    {
        public IDictionary<int, DigitalOutJsonState> DigitalOut { get; set; }

        public VkModuleI2O2OutJsonState()
        {
            DigitalOut = new Dictionary<int, DigitalOutJsonState>();
        }
    }
}