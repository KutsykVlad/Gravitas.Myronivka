using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Card.DAO
{
    public class Card : BaseEntity<string>
    {
        public CardType TypeId { get; set; }
        public int No { get; set; }
        public bool IsActive { get; set; }
        public Guid? EmployeeId { get; set; }
        public int? TicketContainerId { get; set; }
        public string ParentCardId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TicketContainer TicketContainer { get; set; }
        public virtual Card ParentCard { get; set; }
    }
}