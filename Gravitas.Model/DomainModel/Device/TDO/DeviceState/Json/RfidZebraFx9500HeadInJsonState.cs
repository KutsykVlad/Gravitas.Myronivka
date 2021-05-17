using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json
{
    public class RfidZebraFx9500HeadInJsonState
    {
        public IDictionary<int, RfidZebraFx9500AntennaInJsonState> AntenaState { get; set; }
    }
}