using System.Collections.Generic;

namespace Gravitas.CollisionCoordination.Models
{
    public class CollisionData
    {
        public int? TicketId { get; set; } 
        public List<string> PhoneList { get; set; }
        public List<string> EmailList { get; set; }
        public int? TemplateId { get; set; }
    }
}