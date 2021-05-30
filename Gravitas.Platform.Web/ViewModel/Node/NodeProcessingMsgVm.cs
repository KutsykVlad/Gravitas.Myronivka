using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.Node
{
    public class NodeProcessingMsgVm
    {
        public ICollection<NodeProcessingMsgItemVm> Items { get; set; } = new List<NodeProcessingMsgItemVm>();
    }
}