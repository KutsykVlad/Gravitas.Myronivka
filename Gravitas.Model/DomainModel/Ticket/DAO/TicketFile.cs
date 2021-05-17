using System;

namespace Gravitas.Model {
	
    public class TicketFile : BaseEntity<long> {

        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime? DateTime { get; set; }
        
        public long TicketId { get; set; }
        public long TypeId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual TicketFileType TicketFileType { get; set; }
    }
}
