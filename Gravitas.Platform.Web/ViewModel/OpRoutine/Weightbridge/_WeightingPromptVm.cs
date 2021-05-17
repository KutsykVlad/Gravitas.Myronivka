using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class WeightbridgeVms
    {
        public class WeightingPromptVm: BaseWeightPromptVm
        {
            public bool IsTrailerAvailable { get; set; }
            public bool IsSecondWeighting { get; set; }

            [DisplayName("Тара")]
            public double? TareValue { get; set; }
            [DisplayName("Тара причепу")]
            public double? TrailerTareValue { get; set; }

            [DisplayName("Брутто")]
            public double? GrossValue { get; set; }
            [DisplayName("Брутто причепу")]
            public double? TrailerGrossValue { get; set; }


            [DisplayName("Поточне нетто")]
            public double? CurrentDocNetValue { get; set; }

            [DisplayName("Дельта зважування")]
            public double? WeightingDelta { get; set; }

            public WeightingPromptVm(BaseWeightPromptVm baseWeightPromptVm)
            {
                NodeId = baseWeightPromptVm.NodeId;
                ScaleOpTypeName = baseWeightPromptVm.ScaleOpTypeName;
                DriverName = baseWeightPromptVm.DriverName;
                TruckNo = baseWeightPromptVm.TruckNo;
                TrailerNo = baseWeightPromptVm.TrailerNo;
                ProductName = baseWeightPromptVm.ProductName;
                ReceiverName = baseWeightPromptVm.ReceiverName;
                IncomeDocGrossValue = baseWeightPromptVm.IncomeDocGrossValue;
                IncomeDocTareValue = baseWeightPromptVm.IncomeDocTareValue;
                DocNetValue = baseWeightPromptVm.DocNetValue;

            }

            protected WeightingPromptVm()
            {
                
            }
        }
    }
    
}