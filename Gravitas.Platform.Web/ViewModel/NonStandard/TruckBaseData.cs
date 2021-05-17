using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.NonStandard
{
    public class TruckBaseData
    { 
       [DisplayName("Номер ТТН")]
       public string IncomeInvoiceNumber { get; set; }
       
       [DisplayName("Номер автомобіля")]
       public string TruckNo { get; set; }
       
       [DisplayName("Номер причіпа")]
       public string TrailerNo { get; set; }
       
       [DisplayName("Перевізник")]
       public string Carrier { get; set; }
       
       [DisplayName("Водій 1")]
       public string Driver1 { get; set; }
       
       [DisplayName("Водій 2")]
       public string Driver2 { get; set; }
       
       [DisplayName("Номенклатура")]
       public string Product { get; set; }
    }
}