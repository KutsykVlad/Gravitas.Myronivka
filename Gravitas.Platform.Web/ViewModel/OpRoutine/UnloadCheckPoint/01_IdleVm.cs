namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class UnloadCheckPointVms
    {
        public class IdleVm
        {
            public int NodeId { get; set; }
            public LoadPointTicketContainerItemVm BindedTruck { get; set; }
        }
    }
}