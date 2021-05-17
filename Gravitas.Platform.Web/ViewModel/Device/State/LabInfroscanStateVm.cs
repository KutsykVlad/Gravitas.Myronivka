using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class DeviceStateVms
    {
        public class LabInfroscanStateVm
        {
            [DisplayName("Білок")]
            public double? ProteinValue { get; set; }

            [DisplayName("Жир")]
            public double? FatValue { get; set; }
            
            [DisplayName("Волога")]
            public double? HumidityValue { get; set; }
            
            [DisplayName("Клітковина")]
            public double? CelluloseValue { get; set; }

            [DisplayName("Дата\\Час")]
            public DateTime? Date { get; set; }
        }
    }
}