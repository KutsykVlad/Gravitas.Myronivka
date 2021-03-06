using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Ticket.DAO
{
    public class Ticket : BaseEntity<int>
    {
        public Ticket()
        {
            LabFacelessOpDataSet = new HashSet<LabFacelessOpData>();
            SecurityCheckInOpDataSet = new HashSet<SecurityCheckInOpData>();
            SecurityCheckOutOpDataSet = new HashSet<SecurityCheckOutOpData>();
            SecurityCheckReviewOpDataSet = new HashSet<SecurityCheckReviewOpData>();
            ScaleOpDataSet = new HashSet<ScaleOpData>();
            UnloadPointOpDataSet = new HashSet<UnloadPointOpData>();
            UnloadGuideOpDataSet = new HashSet<UnloadGuideOpData>();
            LoadPointOpDataSet = new HashSet<LoadPointOpData>();
            LoadGuideOpDataSet = new HashSet<LoadGuideOpData>();
            TicketFileSet = new HashSet<TicketFile>();
            CentralLabOpDataSet = new HashSet<CentralLabOpData>();
            NonStandartOpDataSet = new HashSet<NonStandartOpData>();
        }

        public int TicketContainerId { get; set; }
        public TicketStatus StatusId { get; set; }
        public int OrderNo { get; set; }
        public int? SecondaryRouteTemplateId { get; set; }
        public int SecondaryRouteItemIndex { get; set; }
        public int? RouteTemplateId { get; set; }
        public int RouteItemIndex { get; set; }
        public RouteType RouteType { get; set; }
        public int? NodeId { get; set; }

        // Navigation Properties
        public virtual Node.DAO.Node Node { get; set; }
        public virtual TicketContainer TicketContainer { get; set; }
        public virtual RouteTemplate RouteTemplate { get; set; }
        public virtual ICollection<LabFacelessOpData> LabFacelessOpDataSet { get; set; }
        public virtual ICollection<SingleWindowOpData> SingleWindowOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckInOpData> SecurityCheckInOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckOutOpData> SecurityCheckOutOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckReviewOpData> SecurityCheckReviewOpDataSet { get; set; }
        public virtual ICollection<CentralLabOpData> CentralLabOpDataSet { get; set; }
        public virtual ICollection<ScaleOpData> ScaleOpDataSet { get; set; }
        public virtual ICollection<UnloadPointOpData> UnloadPointOpDataSet { get; set; }
        public virtual ICollection<UnloadGuideOpData> UnloadGuideOpDataSet { get; set; }
        public virtual ICollection<LoadPointOpData> LoadPointOpDataSet { get; set; }
        public virtual ICollection<LoadGuideOpData> LoadGuideOpDataSet { get; set; }
        public virtual ICollection<TicketFile> TicketFileSet { get; set; }
        public virtual ICollection<NonStandartOpData> NonStandartOpDataSet { get; set; }
    }
}