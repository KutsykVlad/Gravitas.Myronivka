using System.Collections.Generic;

namespace Gravitas.Model {
	
    public class TicketFileType : BaseEntity<long> {

        public TicketFileType() {
            TicketFileSet = new HashSet<TicketFile>();
        }

        public string Name { get; set; }

        public virtual ICollection<TicketFile> TicketFileSet { get; set; }
    }
}
