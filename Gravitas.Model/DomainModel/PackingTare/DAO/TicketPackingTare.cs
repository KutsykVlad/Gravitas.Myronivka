using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model.DomainModel.PackingTare.DAO
{
    [Table("TicketPackingTare")]
    public class TicketPackingTare : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public long PackingTareId { get; set; }
        public int Count { get; set; }
        
        public virtual PackingTare PackingTare { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
