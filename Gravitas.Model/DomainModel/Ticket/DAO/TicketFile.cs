using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    public class TicketFile : BaseEntity<int>
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime? DateTime { get; set; }
        public int TicketId { get; set; }
        public TicketFileType TypeId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}