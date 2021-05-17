namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class MixedFeedLoadVms
    {
        public class IdleVm
        {
            public long NodeId { get; set; }
            public bool IsActive { get; set; }
            public bool PartLoadUnload { get; set; }
            public MixedFeedLoadTicketContainerItemVm BindedTruck { get; set; }
        }
    }
}