using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class DeviceStateVms
    {
        public class DocNetValueStateVm
        {
            [DisplayName("Поточне нетто")]
            public double CurrentDocNetValue { get; set; }
        }
    }
}