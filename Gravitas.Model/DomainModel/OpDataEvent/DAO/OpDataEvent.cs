using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpDataEvent.DAO
{
    public class OpDataEvent: BaseEntity<int>
    {
        public long TicketId { get; set; }
        public long NodeId { get; set; }
        public string EmployeeId { get; set; }
        public int OpDataEventType { get; set; }
        public DateTime Created { get; set; }
        public string TypeOfTransaction { get; set; }
        public string Cause { get; set; }
        public double? Weight { get; set; }
        
        public Node Node { get; set; }
        public ExternalData.Employee Employee { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}