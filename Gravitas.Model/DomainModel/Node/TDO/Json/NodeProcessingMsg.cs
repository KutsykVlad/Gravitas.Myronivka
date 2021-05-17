using System.Collections.Generic;
using Gravitas.Model.Dto;

namespace Gravitas.Model.DomainModel.Node.TDO.Json
{
    public class NodeProcessingMsg
    {
        public ICollection<NodeProcessingMsgItem> Items { get; set; }

        public NodeProcessingMsg()
        {
            Items = new List<NodeProcessingMsgItem>();
        }
    }
}