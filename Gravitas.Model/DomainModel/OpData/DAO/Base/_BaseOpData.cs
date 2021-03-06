using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO.Base
{
    public class BaseOpData : BaseEntity<Guid>
    {
        public BaseOpData()
        {
            OpVisaSet = new HashSet<OpVisa.DAO.OpVisa>();
            OpCameraSet = new HashSet<OpCameraImage.OpCameraImage>();
        }

        public DomainValue.OpDataState StateId { get; set; }

        public int? NodeId { get; set; }
        public int? TicketId { get; set; }
        public int? TicketContainerId { get; set; }

        public DateTime? CheckInDateTime { get; set; }
        public DateTime? CheckOutDateTime { get; set; }

        // Navigation properties
        public virtual Node.DAO.Node Node { get; set; }
        public virtual Ticket.DAO.Ticket Ticket { get; set; }
        public virtual ICollection<OpVisa.DAO.OpVisa> OpVisaSet { get; set; }
        public virtual ICollection<OpCameraImage.OpCameraImage> OpCameraSet { get; set; }
    }
}