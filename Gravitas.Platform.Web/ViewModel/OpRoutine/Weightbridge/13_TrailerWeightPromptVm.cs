namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class WeightbridgeVms
    {
        public class TrailerWeightPromptVm : WeightingPromptVm
        {
            public bool IsSelectionAvailable { get; set; }
            public bool IsRejectButtonDisabled { get; set; }

            public TrailerWeightPromptVm()
            {
            }

            public TrailerWeightPromptVm(WeightingPromptVm baseWeightPromptVm) : base(baseWeightPromptVm)
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