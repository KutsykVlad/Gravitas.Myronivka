namespace Gravitas.Platform.Web.ViewModel.Workstation {
    public class WorkstationStateItemVm {
        public long NodeId { get; set; }
        public string NodeName { get; set; }
        public long? StateId { get; set; }
        public string StateName { get; set; }
        public long NodeState { get; set; }
        public string TransportNo { get; set; }
        public string TrailerNo { get; set; }
        public bool IsCleanupMode { get; set; }
    }
}