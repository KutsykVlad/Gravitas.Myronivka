using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public static partial class ExternalData {
		public class StockDetail : BaseEntity<string> {

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public string Address { get; set; }
		}
	}

}