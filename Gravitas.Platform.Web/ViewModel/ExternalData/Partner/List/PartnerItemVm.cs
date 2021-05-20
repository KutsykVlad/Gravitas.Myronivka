using Gravitas.Platform.Web.ViewModel.Device._Base;

namespace Gravitas.Platform.Web.ViewModel.ExternalData.Partner.List
{
    public class PartnerItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsFolder { get; set; }
    }
}