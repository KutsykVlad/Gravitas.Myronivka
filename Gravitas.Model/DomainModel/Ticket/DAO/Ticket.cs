using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.Model {
	
	public class Ticket : BaseEntity<int> {

		public Ticket() {
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
			MixedFeedGuideOpDataSet = new HashSet<MixedFeedGuideOpData>();
			MixedFeedLoadOpDataSet = new HashSet<MixedFeedLoadOpData>();
			NonStandartOpDataSet = new HashSet<NonStandartOpData>();

        }

		public long ContainerId { get; set; }
		public long StatusId { get; set; }
		public long OrderNo { get; set; }
		public long? SecondaryRouteTemplateId { get; set; }
		public int SecondaryRouteItemIndex { get; set; }
		public long? RouteTemplateId { get; set; }
		public int RouteItemIndex { get; set; }
		public int RouteType { get; set; }
		public long? NodeId { get; set; }

		// Navigation Properties
		public virtual Node Node { get; set; }
		public virtual TicketStatus TicketStatus { get; set; }
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
		public virtual ICollection<MixedFeedLoadOpData> MixedFeedLoadOpDataSet { get; set; }
		public virtual ICollection<MixedFeedGuideOpData> MixedFeedGuideOpDataSet { get; set; }
		public virtual ICollection<NonStandartOpData> NonStandartOpDataSet { get; set; }
  }
}
