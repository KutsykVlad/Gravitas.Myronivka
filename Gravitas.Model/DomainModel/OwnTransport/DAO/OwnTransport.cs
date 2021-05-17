using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OwnTransport.DAO
{
    [Table("OwnTransport")]
    public class OwnTransport : BaseEntity<int>
    {
        public string CardId { get; set; }
        public string TruckNo { get; set; }
        public string TrailerNo { get; set; }
        
        public virtual Card.DAO.Card Card { get; set; }

    }
}