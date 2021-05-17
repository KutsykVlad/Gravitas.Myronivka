using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class QueuePatternVm
    {
        public List<QueuePatternItemVm> Items { get; set; }
        
        public int Count { get; set; }
    }
}