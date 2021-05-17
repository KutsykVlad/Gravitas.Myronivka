﻿using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.PhoneInformTicketAssignment.DAO
{
    public class PhoneInformTicketAssignment : BaseEntity<int>
    {
        public long PhoneDictionaryId { get; set; }
        public PhoneDictionary PhoneDictionary { get; set; }
        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
