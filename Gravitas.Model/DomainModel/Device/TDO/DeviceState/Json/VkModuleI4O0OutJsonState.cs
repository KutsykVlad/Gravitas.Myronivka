using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class VkModuleI4O0OutJsonState : BaseJsonConverter<VkModuleI4O0OutJsonState> {

		public IDictionary<int, DigitalOutJsonState> DigitalOut { get; set; }
	}
}

