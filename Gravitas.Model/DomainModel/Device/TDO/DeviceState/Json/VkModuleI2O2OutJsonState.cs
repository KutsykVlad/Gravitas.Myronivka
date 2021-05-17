using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class VkModuleI2O2OutJsonState : BaseJsonConverter<VkModuleI2O2OutJsonState> {

		public IDictionary<int, DigitalOutJsonState> DigitalOut { get; set; }

		public VkModuleI2O2OutJsonState() {
			DigitalOut = new Dictionary<int, DigitalOutJsonState>();
		}
	}
}

