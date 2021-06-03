using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel.Shared
{
    public class ActionLinkVm
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? NodeId { get; set; }
        public SingleWindowRegisterFilter SingleWindowRegisterFilter { get; set; }
    }
}