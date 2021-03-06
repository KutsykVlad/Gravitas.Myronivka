using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;

namespace Gravitas.Model.DomainModel.OpDataEvent.DAO
{
    public class OpDataEvent: BaseEntity<int>
    {
        public int TicketId { get; set; }
        public int NodeId { get; set; }
        public string EmployeeId { get; set; }
        public int OpDataEventType { get; set; }
        public DateTime Created { get; set; }
        public string TypeOfTransaction { get; set; }
        public string Cause { get; set; }
        public double? Weight { get; set; }
        
        public virtual Node.DAO.Node Node { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Ticket.DAO.Ticket Ticket { get; set; }
    }
}