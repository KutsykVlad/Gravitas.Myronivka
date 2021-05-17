using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model.DomainModel.OwnTransport.DAO
{
    [Table("OwnTransport")]
    public class OwnTransport : BaseEntity<long>
    {
        public string CardId { get; set; }
        public string TruckNo { get; set; }
        public string TrailerNo { get; set; }
        
        public virtual Card Card { get; set; }

    }
}