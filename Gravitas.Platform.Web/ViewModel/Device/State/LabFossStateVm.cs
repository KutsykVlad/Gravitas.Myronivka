using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class DeviceStateVms
    {
        public class LabFossStateVm
        {
            [DisplayName("Дата\\Час")] 
            public DateTime? AnalysisTime { get; set; }

            [DisplayName("Продукт")] 
            public string ProductName { get; set; }

            [DisplayName("Вологість")] 
            public double? HumidityValue { get; set; }

            [DisplayName("Протеїн")] 
            public double? ProteinValue { get; set; }

            [DisplayName("Масличність")] 
            public double? OilValue { get; set; }

            [DisplayName("Fibre")]
            public double? FibreValue { get; set; }
        }
    }
}