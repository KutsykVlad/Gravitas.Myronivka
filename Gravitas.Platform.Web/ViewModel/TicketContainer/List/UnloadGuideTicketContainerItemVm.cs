using System.ComponentModel;
using Gravitas.Platform.Web.Manager;

namespace Gravitas.Platform.Web.ViewModel {

    public class UnloadGuideTicketContainerItemVm {

	    public BaseRegistryData BaseData { get; set; }

		[DisplayName("Дом %")]
        public float? ImpurityValue { get; set; }
		[DisplayName("Вол %")]
        public float? HumidityValue { get; set; }
        [DisplayName("Прот.")]
        public float? EffectiveValue { get; set; }

	    [DisplayName("Норматив погрузки, кг.")]
	    public double LoadTarget { get; set; }

	    [DisplayName("Плюс, кг.")]
	    public int LoadTargetDeviationPlus { get; set; }

	    [DisplayName("Мінус, кг.")]
	    public int LoadTargetDeviationMinus { get; set; }

		[DisplayName("Точка вивантаження")]
        public long UnloadNodeId { get; set; }
		[DisplayName("Точка вивант.")]
		public string UnloadNodeName { get; set; }

        public bool IsActive { get; set; }
	}
}