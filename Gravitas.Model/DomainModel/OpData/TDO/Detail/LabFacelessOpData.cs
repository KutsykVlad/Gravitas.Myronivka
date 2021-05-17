using System;
using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class LabFacelessOpData : BaseOpDataDetail {

		public string ImpurityClassId { get; set; }
		public float? ImpurityValue { get; set; }
		public string HumidityClassId { get; set; }
		public float? HumidityValue { get; set; }
		public string InfectionedClassId { get; set; }
		public float? EffectiveValue { get; set; }
		public string Comment { get; set; }
		public string DataSourceName { get; set; }

		public ICollection<LabFacelessOpDataComponent> LabFacelessOpDataItemSet { get; set; }
	}

	public class LabFacelessOpDataComponent {

		public long Id { get; set; }
		public DateTime? CheckInDateTime { get; set; }

		public Guid LabFacelessOpDataId { get; set; }
		public long StateId { get; set; }
		public long NodeId { get; set; }

		public string AnalysisTrayRfid { get; set; }
		public AnalysisValueDescriptor AnalysisValueDescriptor { get; set; }

		public string ImpurityClassId { get; set; }
		public float? ImpurityValue { get; set; }
		public string HumidityClassId { get; set; }
		public float? HumidityValue { get; set; }
		public string InfectionedClassId { get; set; }
		public float? EffectiveValue { get; set; }
		public string Comment { get; set; }
		public string DataSourceName { get; set; }
	}
}
