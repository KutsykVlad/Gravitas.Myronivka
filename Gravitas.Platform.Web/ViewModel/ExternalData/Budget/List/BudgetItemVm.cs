namespace Gravitas.Platform.Web.ViewModel
{
    public class BudgetItemVm : BaseEntityVm<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
    }
}