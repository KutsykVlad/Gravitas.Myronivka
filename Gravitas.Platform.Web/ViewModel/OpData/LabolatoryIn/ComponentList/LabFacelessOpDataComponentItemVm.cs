using System;
using System.ComponentModel;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LaboratoryInVms
    {
        public class LabFacelessOpDataComponentItemVm
        {
            public int Id { get; set; }
            public DateTime? CheckInDateTime { get; set; }

            public Guid LabFacelessOpDataId { get; set; }
            public OpDataState StateId { get; set; }
            [DisplayName("Статус")]
            public string StateName { get; set; }

            public string AnalysisTrayRfid { get; set; }
            public AnalysisValueDescriptorVm AnalysisValueDescriptor { get; set; }

            [DisplayName("Засміченість, класифікатор")]
            public string ImpurityClassName { get; set; }

            [DisplayName("Засміченість, %")] 
            public float? ImpurityValue { get; set; }

            [DisplayName("Вологість, класифікатор")]
            public string HumidityClassName { get; set; }

            [DisplayName("Вологість, %")] 
            public float? HumidityValue { get; set; }
            [DisplayName("Зараженість")] 
            public string IsInfectionedClassId { get; set; }
            [DisplayName("Масличність/Протеїн")]
            public float? EffectiveValue { get; set; }
            [DisplayName("Коментар")] 
            public string Comment { get; set; }
            [DisplayName("Джерело даних")] 
            public string DataSourceName { get; set; }
        }
    }
}