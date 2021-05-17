namespace Gravitas.Platform.Web.ViewModel
{
    public class FixedAssetItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string TypeCode { get; set; }
        public string RegistrationNo { get; set; }
    }
}