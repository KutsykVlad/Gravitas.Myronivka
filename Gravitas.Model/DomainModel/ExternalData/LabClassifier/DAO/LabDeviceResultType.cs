using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {

	public static partial class ExternalData {

		public class LabDeviceResultType : BaseEntity<string> {

			public string Name { get; set; }
		}
	}
}