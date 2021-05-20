using Gravitas.Platform.Web.ViewModel.Device._Base;

namespace Gravitas.Platform.Web.ViewModel.ExternalData.Budget.List
{
    public class BudgetItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
    }
}