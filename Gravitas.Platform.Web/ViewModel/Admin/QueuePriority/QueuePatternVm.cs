using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.Admin.QueuePriority
{
    public class QueuePatternVm
    {
        public List<QueuePatternItemVm> Items { get; set; }
        
        public int Count { get; set; }
    }
}