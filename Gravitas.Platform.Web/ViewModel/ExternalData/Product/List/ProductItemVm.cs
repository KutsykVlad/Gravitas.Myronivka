using Gravitas.Platform.Web.ViewModel.Device._Base;

namespace Gravitas.Platform.Web.ViewModel.ExternalData.Product.List
{
    public class ProductItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool IsFolder { get; set; }
    }
}