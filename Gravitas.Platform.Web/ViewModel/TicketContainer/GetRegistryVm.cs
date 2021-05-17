using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer
{
    public class GetRegistryVm
    {
        public TicketRegisterType RegisterType { get; set; }
        public long NodeId { get; set; }
        public ActionLinkVm DetailActionLink { get; set; }
    }
}