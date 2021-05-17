using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class DeviceStateVms
    {
        public class WeighbridgeStateVm
        {
            public bool ScaleIsError { get; set; }
            public bool ScaleIsZero { get; set; }
            public bool ScaleIsImmobile { get; set; }
            public bool ScaleIsGross { get; set; }
            public DateTime? ScaleTime { get; set; }
            public double ScaleValue { get; set; }


            public bool PerimeterLeft { get; set; }
            public bool PerimeterRight { get; set; }
            public bool PerimeterTop { get; set; }
            public bool PerimeterBottom { get; set; }
        }
    }
}