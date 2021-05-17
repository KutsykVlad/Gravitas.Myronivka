using System;

namespace Gravitas.Model.Dto {

	public class ScaleInJsonState : BaseJsonConverter<ScaleInJsonState> {
		public double Value { get; set; }
		public bool IsZero { get; set; }
		public bool IsImmobile { get; set; }
		public bool IsGross { get; set; }
		public bool IsScaleError { get; set; }
	}
}
