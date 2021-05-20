using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.PhoneInformTicketAssignment.DAO
{
    public class PhoneInformTicketAssignment : BaseEntity<int>
    {
        public Phone PhoneDictionaryId { get; set; }
        public int TicketId { get; set; }
        
        public PhoneDictionary.DAO.PhoneDictionary PhoneDictionary { get; set; }
        public Ticket.DAO.Ticket Ticket { get; set; }
    }
}
