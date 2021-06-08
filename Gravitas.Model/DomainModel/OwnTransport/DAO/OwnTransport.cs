using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.OwnTransport.DAO
{
    [Table("OwnTransport")]
    public class OwnTransport : BaseEntity<int>
    {
        public string CardId { get; set; }
        public string LongRangeCardId { get; set; }
        public string TruckNo { get; set; }
        public string TrailerNo { get; set; }
        public string Driver { get; set; }
        public OwnTransportType TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        public virtual Card.DAO.Card Card { get; set; }
        public virtual Card.DAO.Card LongRangeCard { get; set; }
    }
}