using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class DeviceStateVms
    {
        public class LabBrukerStateVm
        {
            [DisplayName("Проба")] 
            public string SampleName { get; set; }

            [DisplayName("Маса")] 
            public double? SampleMass { get; set; }

            [DisplayName("Результат 1")] 
            public double? Result1 { get; set; }

            [DisplayName("Результат 2")] 
            public double? Result2 { get; set; }

            [DisplayName("Дата\\Час")] 
            public DateTime? AcquisitionDate { get; set; }

            [DisplayName("Коментар")] 
            public string Comment { get; set; }
        }
    }
}