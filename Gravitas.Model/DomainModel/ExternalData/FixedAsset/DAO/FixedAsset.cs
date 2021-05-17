using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {

	public static partial class ExternalData {
		public class FixedAsset : BaseEntity<string> {
			
			public string Code { get; set; }
			public string Brand { get; set; }
			public string Model { get; set; }
			public string TypeCode { get; set; }
			public string RegistrationNo { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }
		}
	}
}