namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class UnloadPointType2Vms
    {
        public class IdleVm
        {
            public int NodeId { get; set; }
            public UnloadPointTicketContainerItemVm BindedTruck { get; set; }
        }
    }
}