using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.Model.DomainModel.PredefinedRoute.DAO
{
    public class RouteTemplate : BaseEntity<int>
    {
        public RouteTemplate()
        {
            TicketSet = new HashSet<Ticket.DAO.Ticket>();
            SingleWindowOpDataSet = new HashSet<SingleWindowOpData>();
        }

        public string Name { get; set; }
        public string RouteConfig { get; set; }
        public string OwnerId { get; set; }
        public bool IsMain { get; set; }
        public bool IsInQueue { get; set; }
        public bool IsTechnological { get; set; }

        public virtual ICollection<Ticket.DAO.Ticket> TicketSet { get; set; }
        public virtual ICollection<SingleWindowOpData> SingleWindowOpDataSet { get; set; }
    }
}