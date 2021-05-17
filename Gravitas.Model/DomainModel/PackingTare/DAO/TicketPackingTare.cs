using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.PackingTare.DAO
{
    [Table("TicketPackingTare")]
    public class TicketPackingTare : BaseEntity<int>
    {
        public int TicketId { get; set; }
        public int PackingTareId { get; set; }
        public int Count { get; set; }
        
        public virtual PackingTare PackingTare { get; set; }
        public virtual Ticket.DAO.Ticket Ticket { get; set; }
    }
}
