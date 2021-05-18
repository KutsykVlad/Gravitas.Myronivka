using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;

namespace Gravitas.Model.DomainModel.Card.DAO
{
    public class Card : BaseEntity<string>
    {
        public int TypeId { get; set; }
        public int No { get; set; }
        public bool IsActive { get; set; }
        public bool IsOwn { get; set; }
        public string EmployeeId { get; set; }
        public int? TicketContainerId { get; set; }
        public string ParentCardId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TicketContainer TicketContainer { get; set; }
        public virtual CardType CardType { get; set; }
    }
}