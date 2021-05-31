using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class WeightbridgeVms
    {
        public class BaseWeightPromptVm
        {
            public int NodeId { get; set; }

            [DisplayName("Тип зважування")]
            public string ScaleOpTypeName { get; set; }

            [DisplayName("Водій")]
            public string DriverName { get; set; }

            [DisplayName("Номер вантажівки")]
            public string TruckNo { get; set; }

            [DisplayName("Номер причепу")]
            public string TrailerNo { get; set; }

            [DisplayName("Номенклатура")]
            public string ProductName { get; set; }

            [DisplayName("Відправник/одержувач")]
            public string ReceiverName { get; set; }

            // 76
            [DisplayName("Брутто (За документами)")]
            public double? IncomeDocGrossValue { get; set; }

            // 77
            [DisplayName("Тара (За документами)")]
            public double? IncomeDocTareValue { get; set; }

            // 58
            [DisplayName("Нетто (за документами)")]
            public double? DocNetValue { get; set; }

            public string ValidationMessage { get; set; }
        }
    }
}