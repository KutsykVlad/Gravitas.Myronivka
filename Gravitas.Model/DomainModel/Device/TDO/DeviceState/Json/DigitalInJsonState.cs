using System;
using System.Xml.Serialization;

namespace Gravitas.Model.Dto {

	public class DigitalInJsonState : BaseJsonConverter<DigitalInJsonState>
	{
		public bool Value { get; set; }
	}
}

