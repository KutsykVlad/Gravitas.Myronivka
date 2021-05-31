using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel 
{
	public static partial class LaboratoryInVms 
	{
		public class PrintAnalysisResultsVm 
		{
			public int NodeId { get; set; }
			public Guid OpDataId { get; set; }
			public int OpDataComponentId { get; set; }

			// 13
			[DisplayName("Номенклатура")]
			public string ProductName { get; set; }
			// 56
			[DisplayName("Вологість, %")]
			public float? DocHumidityValue { get; set; }
			// 57
			[DisplayName("Засміченість, %")]
			public float? DocImpurityValue { get; set; }

			public LabFacelessOpDataDetailVm LabFacelessOpDataDetail { get; set; }
		}
	}
}