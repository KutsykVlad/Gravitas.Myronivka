namespace Gravitas.Platform.Web.ViewModel.Workstation
{
    public class WorkstationStateItemVm
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int NodeState { get; set; }
        public string TransportNo { get; set; }
        public string TrailerNo { get; set; }
        public bool IsCleanupMode { get; set; }
    }
}