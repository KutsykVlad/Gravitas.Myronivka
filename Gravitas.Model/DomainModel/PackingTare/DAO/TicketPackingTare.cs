using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.PackingTare.DAO
{
    [Table("TicketPackingTare")]
    public class TicketPackingTare : BaseEntity<int>
    {
        public long TicketId { get; set; }
        public long PackingTareId { get; set; }
        public int Count { get; set; }
        
        public virtual PackingTare PackingTare { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
