namespace Gravitas.Platform.Web.ViewModel
{
    public class StockItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}