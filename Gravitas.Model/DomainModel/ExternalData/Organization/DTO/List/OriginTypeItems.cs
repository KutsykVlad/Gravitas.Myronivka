using System.Collections.Generic;

namespace Gravitas.Model.Dto {

	public static partial class ExternalData {

		public class OriginTypeItems : BaseEntity<string> {

			public IEnumerable<OriginTypeItem>	Items { get; set; }
		}
	}
}