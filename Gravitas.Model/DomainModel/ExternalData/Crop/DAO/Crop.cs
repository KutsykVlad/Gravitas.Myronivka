namespace Gravitas.Model {

	public static partial class ExternalData {

		public class Crop : BaseEntity<string> {

			public string Code { get; set; }
			public string Name { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }
		}
	}
}
