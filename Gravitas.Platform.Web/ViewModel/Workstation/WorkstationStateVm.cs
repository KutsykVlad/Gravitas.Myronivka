using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.Workstation
{
    public class WorkstationStateVm
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int CurrentNodeId { get; set; }
        public IEnumerable<WorkstationStateItemVm> Items { get; set; }
    }
}