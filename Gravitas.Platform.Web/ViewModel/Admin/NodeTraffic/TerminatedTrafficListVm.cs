using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class TerminatedTrafficListVm
    {
        public ICollection<NodeTrafficItem> Items { get; set; } = new List<NodeTrafficItem>();
    }
}