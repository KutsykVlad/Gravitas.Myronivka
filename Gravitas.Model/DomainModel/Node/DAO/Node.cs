using System.Collections.Generic;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model {

	public class Node : BaseEntity<long> {

		public Node() {
			NodeConstraintSet = new HashSet<NodeConstraint>();

			LabFacelessOpDataSet = new HashSet<LabFacelessOpData>();
			SecurityCheckInOpDataSet = new HashSet<SecurityCheckInOpData>();
			SecurityCheckOutOpDataSet = new HashSet<SecurityCheckOutOpData>();
			SecurityCheckReviewOpDataSet = new HashSet<SecurityCheckReviewOpData>();
			ScaleOpDataSet = new HashSet<ScaleOpData>();
			UnloadPointOpDataSet = new HashSet<UnloadPointOpData>();
			UnloadGuideOpDataSet = new HashSet<UnloadGuideOpData>();
			LoadPointOpDataSet = new HashSet<LoadPointOpData>();
			LoadGuideOpDataSet = new HashSet<LoadGuideOpData>();
			LoadPointSet = new HashSet<LoadGuideOpData>();
			NonStandartOpDataSet = new HashSet<NonStandartOpData>();
		    TrafficHistory = new HashSet<TrafficHistory>();
            Assignments = new HashSet<RoleAssignment>();
		    CentralLabOpSet = new HashSet<CentralLabOpData>();
			MixedFeedGuideOpDataSet = new HashSet<MixedFeedGuideOpData>();
			MixedFeedLoadOpDataSet = new HashSet<MixedFeedLoadOpData>();
		}
		
		public long? OrganisationUnitId { get; set; }
		public long? OpRoutineId { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public bool IsActive { get; set; }
		public bool IsEmergency { get; set; }
		public bool IsStart { get; set; }
		public bool IsFinish { get; set; }
		public int Quota { get; set; }

		public string Config { get; set; }
		public string Context { get; set; }
		public string ProcessingMessage { get; set; }
		
        public long MaximumProcessingTime { get; set; }
        
        public NodeGroup NodeGroup { get; set; }

		//Navigation Properties
		public virtual OrganizationUnit OrganizationUnit { get; set; }
		public virtual OpRoutine OpRoutine { get; set; }
		public virtual ICollection<NodeConstraint> NodeConstraintSet { get; set; }

		public virtual ICollection<LabFacelessOpData> LabFacelessOpDataSet { get; set; }
        public virtual ICollection<CentralLabOpData> CentralLabOpSet { get; set; }
		public virtual ICollection<SingleWindowOpData> SingleWindowOpDataSet { get; set; }
		public virtual ICollection<SecurityCheckInOpData> SecurityCheckInOpDataSet { get; set; }
		public virtual ICollection<SecurityCheckOutOpData> SecurityCheckOutOpDataSet { get; set; }
		public virtual ICollection<SecurityCheckReviewOpData> SecurityCheckReviewOpDataSet { get; set; }
		public virtual ICollection<ScaleOpData> ScaleOpDataSet { get; set; }
		public virtual ICollection<UnloadPointOpData> UnloadPointOpDataSet { get; set; }
		public virtual ICollection<UnloadGuideOpData> UnloadGuideOpDataSet { get; set; }		
		public virtual ICollection<LoadPointOpData> LoadPointOpDataSet { get; set; }
		public virtual ICollection<LoadGuideOpData> LoadGuideOpDataSet { get; set; }
		public virtual ICollection<LoadGuideOpData> LoadPointSet { get; set; }
		public virtual ICollection<NonStandartOpData> NonStandartOpDataSet { get; set; }
        public virtual ICollection<TrafficHistory> TrafficHistory { get; set; }
        public virtual ICollection<RoleAssignment> Assignments { get; set; }
		public virtual ICollection<MixedFeedLoadOpData> MixedFeedLoadOpDataSet { get; set; }
		public virtual ICollection<MixedFeedGuideOpData> MixedFeedGuideOpDataSet { get; set; }
	}
}
