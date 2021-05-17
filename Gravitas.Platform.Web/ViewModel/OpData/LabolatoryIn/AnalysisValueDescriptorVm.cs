using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class LaboratoryInVms {

		public class AnalysisValueDescriptorVm {

			[DisplayName("Забрудненість")]
			public bool EditImpurity { get; set; }
			[DisplayName("Вологість")]
			public bool EditHumidity { get; set; }
			[DisplayName("Зараженість")]
			public bool EditIsInfectioned { get; set; }
			[DisplayName("Масличність\\Протеїн")]
			public bool EditEffectiveValue { get; set; }
		}
	}
}