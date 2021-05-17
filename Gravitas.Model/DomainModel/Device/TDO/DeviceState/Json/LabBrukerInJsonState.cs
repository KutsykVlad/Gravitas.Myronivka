using System;

namespace Gravitas.Model.Dto {

	public class LabBrukerInJsonState : BaseJsonConverter<LabBrukerInJsonState> {
		
		public string SampleName { get; set; }
		public double? SampleMass { get; set; }
		public double? Result1 { get; set; }
		public double? Result2 { get; set; }
		public string Unit { get; set; }
		public string Batch { get; set; }
		public string Calibration { get; set; }
		public DateTime? AcquisitionDate { get; set; }
		public string Comment { get; set; }
		public string Valid { get; set; }
		public string Outlier { get; set; }
		public string User { get; set; }
	}
}
