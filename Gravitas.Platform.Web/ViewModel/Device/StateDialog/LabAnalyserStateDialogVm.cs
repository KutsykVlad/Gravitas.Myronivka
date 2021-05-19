using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class DeviceStateVms
    {
        public class LabAnalyserStateDialogVm
        {
            public int? NodeId { get; set; }
            public bool IsActualData { get; set; }

            public int? DeviceId { get; set; }
            public string DeviceName { get; set; }

            [DisplayName("Дата\\Час")] 
            public DateTime? AnalysisTime { get; set; }

            [DisplayName("Проба")] 
            public string SampleName { get; set; }

            public List<LabAnalyserValueVm> ValueList { get; set; }
        }

        public class LabAnalyserValueVm
        {
            [HiddenInput] 
            public int Id { get; set; }

            [DisplayName("Назва показника")] 
            public string Name { get; set; }

            [DisplayName("Значення")] 
            public double? Value { get; set; }

            [DisplayName("Цільове значення")] 
            public string TargetId { get; set; }
        }
    }
}