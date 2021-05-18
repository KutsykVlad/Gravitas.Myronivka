using System;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Node.TDO.Json
{
    public class NodeProcessingMsgItem
    {
        public DateTime? Time { get; set; }
        public int TypeId { get; set; }
        public string Text { get; set; }

        public NodeProcessingMsgItem(int typeId, string text)
        {
            Time = DateTime.Now;
            TypeId = typeId;
            Text = text;
        }
    }
}