using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    public class TicketContainerStatus : BaseEntity<int>
    {
        public TicketContainerStatus()
        {
            TicketContainerSet = new HashSet<TicketContainer>();
        }

        public string Name { get; set; }

        public virtual ICollection<TicketContainer> TicketContainerSet { get; set; }
    }
}