using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public static partial class ExternalData {
		public class RouteDetail : BaseEntity<string> {

			public string Code { get; set; }
			public string Name { get; set; }
		}
	}

}