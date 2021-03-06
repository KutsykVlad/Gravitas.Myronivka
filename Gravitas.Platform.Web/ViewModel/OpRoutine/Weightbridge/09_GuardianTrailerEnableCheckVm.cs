namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class WeightbridgeVms
    {
        public class GuardianTrailerEnableCheckVm : BaseWeightPromptVm
        {
            public GuardianTrailerEnableCheckVm(BaseWeightPromptVm baseWeightPromptVm)
            {
                NodeId = baseWeightPromptVm.NodeId;
                DriverName = baseWeightPromptVm.DriverName;
                TruckNo = baseWeightPromptVm.TruckNo;
                TrailerNo = baseWeightPromptVm.TrailerNo;
                ProductName = baseWeightPromptVm.ProductName;
                ReceiverName = baseWeightPromptVm.ReceiverName;
                IncomeDocGrossValue = baseWeightPromptVm.IncomeDocGrossValue;
                IncomeDocTareValue = baseWeightPromptVm.IncomeDocTareValue;
                DocNetValue = baseWeightPromptVm.DocNetValue;

            }
        }
    }
}