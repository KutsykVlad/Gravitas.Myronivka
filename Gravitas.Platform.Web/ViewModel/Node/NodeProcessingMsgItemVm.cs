using System;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel.Node
{
    public class NodeProcessingMsgItemVm
    {
        public DateTime? Time { get; set; }
        public ProcessingMsgType TypeId { get; set; }
        public string Text { get; set; }
    }
}