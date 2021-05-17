using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model.Dto {

	public class RfidZebraFx9500HeadInJsonState : BaseJsonConverter<RfidZebraFx9500HeadInJsonState> {

		public IDictionary<int, RfidZebraFx9500AntennaInJsonState> AntenaState { get; set; }
	}
}
