using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.Workstation {
    public class WorkstationStateVm {
        public string Name { get; set; }
        public long Id { get; set; }
        public long CurrentNodeId { get; set; }
        public IEnumerable<WorkstationStateItemVm> Items { get; set; }
    }
}