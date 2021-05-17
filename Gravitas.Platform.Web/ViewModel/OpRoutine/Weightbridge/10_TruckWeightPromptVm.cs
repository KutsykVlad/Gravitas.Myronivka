using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class WeightbridgeVms
    {
        public class TruckWeightPromptVm : WeightingPromptVm
        {
            public bool IsSelectionAvailable { get; set; }

            public TruckWeightPromptVm()
            {
            }

            public TruckWeightPromptVm(WeightingPromptVm baseWeightPromptVm) : base(baseWeightPromptVm)
            {
                WeightingDelta = baseWeightPromptVm.WeightingDelta;
                CurrentDocNetValue = baseWeightPromptVm.CurrentDocNetValue;
                TrailerGrossValue = baseWeightPromptVm.TrailerGrossValue;
                GrossValue = baseWeightPromptVm.GrossValue;
                TrailerTareValue = baseWeightPromptVm.TrailerTareValue;
                TareValue = baseWeightPromptVm.TareValue;
                IsSecondWeighting = baseWeightPromptVm.IsSecondWeighting;
                IsTrailerAvailable = baseWeightPromptVm.IsTrailerAvailable;
            }


        }
    }
}