using Gravitas.Model;

namespace Gravitas.Model.Dto {

	public class AnalysisValueDescriptor : BaseJsonConverter<AnalysisValueDescriptor> {

		public bool EditImpurity { get; set; }
		public bool EditHumidity{ get; set; }
		public bool EditIsInfectioned { get; set; }
		public bool EditEffectiveValue { get; set; }
	}
}
