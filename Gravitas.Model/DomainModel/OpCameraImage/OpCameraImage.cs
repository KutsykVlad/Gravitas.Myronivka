using System;

namespace Gravitas.Model {
	
	public class OpCameraImage : BaseEntity<long> {

		public string Source { get; set; }
		public string ImagePath { get; set; }
		public DateTime? DateTime { get; set; }

		public long SourceDeviceId { get; set; }

		public Guid? LabFacelessOpDataId { get; set; }
		public Guid? LabRegularOpDataId { get; set; }
		public Guid? LoadOpDataId { get; set; }
		public Guid? SingleWindowOpDataId { get; set; }
		public Guid? SecurityCheckInOpDataId { get; set; }
		public Guid? SecurityCheckOutOpDataId { get; set; }
		public Guid? ScaleOpDataId { get; set; }
		public Guid? UnloadGuideOpDataId { get; set; } 
		public Guid? UnloadPointOpDataId { get; set; }
		public Guid? LoadGuideOpDataId { get; set; } 
		public Guid? LoadPointOpDataId { get; set; }
		public Guid? NonStandartOpDataId { get; set; }
		public Guid? MixedFeedLoadOpDataId { get; set; }
		public Guid? MixedFeedGuideOpDataId { get; set; }

		public virtual Device Device { get; set; }

		public virtual LabFacelessOpData LabFacelessOpData { get; set; }
		public virtual SingleWindowOpData SingleWindowOpData { get; set; }
		public virtual SecurityCheckInOpData SecurityCheckInOpData { get; set; }
		public virtual SecurityCheckOutOpData SecurityCheckOutOpData { get; set; }
		public virtual ScaleOpData ScaleOpData { get; set; }
		public virtual UnloadGuideOpData UnloadGuideOpData { get; set; }
		public virtual UnloadPointOpData UnloadPointOpData { get; set; }		
		public virtual LoadGuideOpData LoadGuideOpData { get; set; }
		public virtual LoadPointOpData LoadPointOpData { get; set; }
		public virtual NonStandartOpData NonStandartOpData { get; set; }
		public virtual MixedFeedLoadOpData MixedFeedLoadOpData { get; set; }
		public virtual MixedFeedGuideOpData MixedFeedGuideOpData { get; set; }
	}
}
