using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Message.DAO
{
    public class Message : BaseEntity<int>
    {
        public string CardId { get; set; }
        public MessageType TypeId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public string Receiver { get; set; }
        public string AttachmentPath { get; set; }
        public long DeliveryId { get; set; }
        public MessageStatus Status { get; set; }
        public int RetryCount { get; set; }

        public virtual Card.DAO.Card Card { get; set; }
    }
}