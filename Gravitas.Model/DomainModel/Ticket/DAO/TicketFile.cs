using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    public class TicketFile : BaseEntity<int>
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime? DateTime { get; set; }
        public int TicketId { get; set; }
        public int TypeId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual TicketFileType TicketFileType { get; set; }
    }
}