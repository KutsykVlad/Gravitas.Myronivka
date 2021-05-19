using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LaboratoryInVms
    {
        public class SampleBindAnalysisTrayVm
        {
            public int NodeId { get; set; }
            public Guid OpDataId { get; set; }

            [DisplayName("Номенклатура")]
            public string Product { get; set; }

            [DisplayName("Картка")] 
            public string Card { get; set; }

            public AnalysisValueDescriptorVm AnalysisValueDescriptor { get; set; }
        }
    }
}