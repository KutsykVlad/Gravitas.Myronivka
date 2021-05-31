namespace Gravitas.Platform.Web.ViewModel.Node.Detail
{
    public class NodeRoutineVm
    {
        public int NodeId { get; set; }
        public int WorkstationId { get; set; }
        public string NodeCode { get; set; }
        public string NodeName { get; set; }

        public NodeContextVm NodeContextVm { get; set; }
    }
}