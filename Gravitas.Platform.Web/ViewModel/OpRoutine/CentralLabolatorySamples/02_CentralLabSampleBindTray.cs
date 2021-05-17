using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLabolatorySamples
    {
        public class CentralLabSampleBindTrayVm
        {
            public long NodeId { get; set; }
            [DisplayName("Картка")]
            public string CardNumber { get; set; }
            [DisplayName("Транспорт")]
            public string TransportNo { get; set; }
            [DisplayName("Причеп")]
            public string TrailerNo { get; set; }
            [DisplayName("Номенклатура")]
            public string ProductName { get; set; }
            [DisplayName("Перевізник")]
            public bool IsThirdPartyCarrier { get; set; }
        }
    }

}