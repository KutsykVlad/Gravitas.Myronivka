using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.Model.DomainModel.Queue.DAO
{
    public class QueueRegister : BaseEntity<int>
    {
        public DateTime RegisterTime { get; set; }
        public DateTime? SMSTimeAllowed { get; set; }
        public bool IsAllowedToEnterTerritory { get; set; }
        public bool IsSMSSend { get; set; }
        public int RouteTemplateId { get; set; }
        public int TicketContainerId { get; set; }
        public string PhoneNumber { get; set; }
        public string TrailerPlate { get; set; }
        public string TruckPlate { get; set; }
        
        public virtual RouteTemplate RouteTemplate { get; set; }
        public virtual TicketContainer TicketContainer { get; set; }
    }
}
