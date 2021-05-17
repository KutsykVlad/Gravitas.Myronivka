using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gravitas.Model.Dto {

	public class VkModuleI4O0InJsonState : BaseJsonConverter<VkModuleI4O0InJsonState> {
		public IDictionary<int, DigitalInJsonState> DigitalIn { get; set; }
	}
}

