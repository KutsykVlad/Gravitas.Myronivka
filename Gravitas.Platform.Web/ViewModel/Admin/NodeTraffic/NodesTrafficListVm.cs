using System.Linq;

namespace Gravitas.Platform.Web.ViewModel
{ 
    public class NodesTrafficListVm
    {
        public IQueryable<NodeTrafficListVm> Items { get; set; }

        public IQueryable<NodeTrafficItem> TerminatedItems { get; set; }

        public bool WholeList { get; set; }
    }
}