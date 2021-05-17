namespace Gravitas.Model.Dto {

	public static partial class ExternalData {

		public class FixedAssetItem : BaseEntity<string> {

			public string Code { get; set; }
			public string Brand { get; set; }
			public string Model { get; set; }
			public string TypeCode { get; set; }
			public string RegistrationNo { get; set; }
		}
	}
}