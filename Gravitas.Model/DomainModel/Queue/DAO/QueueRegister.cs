using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.PreRegistration.DAO;

namespace Gravitas.Model
{
    public class QueueRegister : BaseEntity<int>
    {
        public DateTime RegisterTime
        {
            get => _registerTime ?? DateTime.Now;
            set => _registerTime = value;
        }

        private DateTime? _registerTime;
        public DateTime? SMSTimeAllowed { get; set; }

        public bool IsAllowedToEnterTerritory { get; set; }
        public bool IsSMSSend { get; set; }

        public long? RouteTemplateId { get; set; }
        public long? TicketContainerId { get; set; }
        public string PhoneNumber { get; set; }
        public string TrailerPlate { get; set; }
        public string TruckPlate { get; set; }
        public long? PreRegisterCompanyId { get; set; }
        
        public virtual RouteTemplate RouteTemplate { get; set; }
        public virtual PreRegisterCompany PreRegisterCompany { get; set; }
    }
}
