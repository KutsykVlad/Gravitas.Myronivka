using System.Collections.Generic;

namespace Gravitas.Core.Valves
{
    public class Valve
    {
        public List<ValveDevice> Devices { get; set; }
        public string Name { get; set; }
    }
    
    public class ValveDevice
    {
        public int DeviceId { get; set; }
        public bool ExpectedResult { get; set; }
    }
}