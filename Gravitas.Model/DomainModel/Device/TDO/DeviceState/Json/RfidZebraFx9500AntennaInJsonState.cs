using System;
using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public class RfidZebraFx9500AntennaInJsonState : BaseJsonConverter<RfidZebraFx9500AntennaInJsonState> {

		public IDictionary<string, DateTime> TagList { get; set; }
	}
}
