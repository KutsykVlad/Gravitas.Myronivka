using System;
using System.Collections.Generic;

namespace Gravitas.Model {

	public class LabFacelessOpData : BaseOpData {
		
		public string ImpurityClassId { get; set; }
		public float? ImpurityValue { get; set; }
		public string HumidityClassId { get; set; }
		public float? HumidityValue { get; set; }
		public string InfectionedClassId { get; set; }
		public float? EffectiveValue { get; set; }
		public string Comment { get; set; }
		public string DataSourceName { get; set; }

		public long? ImpurityValueSourceId { get; set; }
		public long? HumidityValueSourceId { get; set; }
		public long? InfectionedValueSourceId { get; set; }
		public long? EffectiveValueSourceId { get; set; }
		
		public string CollisionComment { get; set; }
		public string LabEffectiveClassifier { get; set; }

		public virtual ICollection<LabFacelessOpDataComponent> LabFacelessOpDataComponentSet { get; set; }
	}

	public class LabFacelessOpDataComponent : BaseEntity<long> {

		public Guid LabFacelessOpDataId { get; set; }
		public long StateId { get; set; }
		public long NodeId { get; set; }
		
		public string AnalysisTrayRfid { get; set; }
		public string AnalysisValueDescriptor { get; set; }

		public DateTime? CheckInDateTime { get; set; }

		public string ImpurityClassId { get; set; }
		public float? ImpurityValue { get; set; }
		public string HumidityClassId { get; set; }
		public float? HumidityValue { get; set; }
		public string InfectionedClassId { get; set; }
		public float? EffectiveValue { get; set; }
		public string Comment { get; set; }
		public string DataSourceName { get; set; }

		public virtual LabFacelessOpData LabFacelessOpData { get; set; }
		public virtual ICollection<OpVisa> OpVisaSet { get; set; }
	}
}
