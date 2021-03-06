using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class TruckBaseInfo
    {
        [DisplayName("Транспорт")]
        public string TransportNo { get; set; }

        [DisplayName("Причіп")]
        public string TrailerNo { get; set; }

        [DisplayName("Номенклатура")]
        public string ProductName { get; set; }
            
        [DisplayName("Пломби")]
        public string StampList { get; set; }
    }
}