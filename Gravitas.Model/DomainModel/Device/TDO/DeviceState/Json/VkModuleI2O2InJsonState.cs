using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gravitas.Model.Dto {

	public class VkModuleI2O2InJsonState : BaseJsonConverter<VkModuleI2O2InJsonState> {
		public IDictionary<int, DigitalInJsonState> DigitalIn { get; set; }
	}
}

