using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Admin.QueuePriority;

namespace Gravitas.Platform.Web.ViewModel
{
    public class QueuePatternVm
    {
        public List<QueuePatternItemVm> Items { get; set; }
        
        public int Count { get; set; }
    }
}