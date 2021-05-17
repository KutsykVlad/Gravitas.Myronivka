using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public static partial class ExternalData {

		public class LabDeviceResultTypeItem : BaseEntity<string> {

			public string Name { get; set; }
		}
	}
}