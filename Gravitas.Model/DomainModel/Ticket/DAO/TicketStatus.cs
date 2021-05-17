using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    public class TicketStatus : BaseEntity<int>
    {
        public TicketStatus()
        {
            TicketSet = new HashSet<Ticket>();
        }

        public string Name { get; set; }

        public virtual ICollection<Ticket> TicketSet { get; set; }
    }
}