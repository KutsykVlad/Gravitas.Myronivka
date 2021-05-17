using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LoadPointGuideVms
    {
        public class BindDestPointVm
        {
            public long NodeId { get; set; }

            [DisplayName("Картка")]
            public string Card { get; set; }

            [DisplayName("Номенклатура")]
            public string ProductName { get; set; }

            [DisplayName("Авто")]
            public string TransportNo { get; set; }

            [DisplayName("Причіп")]
            public string TrailerNo { get; set; }

            [DisplayName("Отримувач")]
            public string ReceiverName { get; set; }

            [DisplayName("Перевізник")]
            public bool IsThirdPartyCarrier { get; set; }

            [DisplayName("Точка призначення")]
            public long DestNodeId { get; set; }

            public string DestNodeName { get; set; }

            [DisplayName("Норматив завантаження/ Довант./ Част. розвант.")]
            public double WeightValue { get; set; }

            [DisplayName("Стороння тара")]
            public double? PackingWeightValue { get; set; }

            [DisplayName("Плюс, кг.")]
            public int LoadTargetDeviationPlus { get; set; }

            [DisplayName("Мінус, кг.")]
            public int LoadTargetDeviationMinus { get; set; }
        }
    }
}