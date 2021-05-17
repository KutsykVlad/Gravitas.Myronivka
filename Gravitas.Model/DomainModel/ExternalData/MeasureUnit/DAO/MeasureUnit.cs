using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {

	public static partial class ExternalData {
		public class MeasureUnit : BaseEntity<string> {

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }
		}
	}
}