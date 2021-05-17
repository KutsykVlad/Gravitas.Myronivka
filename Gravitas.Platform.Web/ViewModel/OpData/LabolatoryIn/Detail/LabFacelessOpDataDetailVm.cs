using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class LaboratoryInVms {

		public class LabFacelessOpDataDetailVm : BaseOpDataDetailVm {

			[DisplayName("Засміченість, класифікатор"), Required(ErrorMessage = "Введіть класифікатор")]
			public string ImpurityClassId { get; set; }
			[DisplayName("Засміченість, %")]
			public float? ImpurityValue { get; set; }
			[DisplayName("Вологість, класифікатор"), Required(ErrorMessage = "Введіть класифікатор")]
			public string HumidityClassId { get; set; }
			[DisplayName("Вологість, %")]
			public float? HumidityValue { get; set; }
			[DisplayName("Зараженість")]
			public string IsInfectionedClassId { get; set; }
			[DisplayName("Масличність/Протеїн")]
			public float? EffectiveValue { get; set; }
			[DisplayName("Перерахунок")]
			public string IsEffectiveClassId { get; set; }
			[DisplayName("Коментар")]
			public string Comment { get; set; }
			[DisplayName("Джерело даних")]
			public string DataSourceName { get; set; }

			public LabFacelessOpDataComponentItemsVm LabFacelessOpDataComponentItemSet { get; set; }
		}
	}
}
