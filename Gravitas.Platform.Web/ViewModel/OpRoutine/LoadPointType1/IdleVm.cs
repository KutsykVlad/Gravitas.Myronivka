namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LoadPointType1Vms
    {
        public class IdleVm
        {
            public int NodeId { get; set; }
            public LoadPointTicketContainerItemVm BindedTruck { get; set; }
            public bool PartLoadUnload { get; set; }
            public bool IsActive { get; set; }
            public SingleWindowVms.ProductContentListVm ProductContentList { get; set; }
        }
    }
}