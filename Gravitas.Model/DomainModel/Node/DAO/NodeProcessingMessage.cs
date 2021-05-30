using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Node.DAO
{
    public class NodeProcessingMessage : BaseEntity<int>
    {
        public int NodeId { get; set; }
        public string Message { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public ProcessingMsgType TypeId { get; set; }

        public virtual Node Node { get; set; }
    }
}