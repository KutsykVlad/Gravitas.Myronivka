using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    public class TicketFileType : BaseEntity<int>
    {
        public TicketFileType()
        {
            TicketFileSet = new HashSet<TicketFile>();
        }

        public string Name { get; set; }

        public virtual ICollection<TicketFile> TicketFileSet { get; set; }
    }
}