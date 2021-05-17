using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.Model {

	public static partial class ExternalData {
		public class Product : BaseEntity<string> {
			public Product()
			{
				MixedFeedSiloSet = new HashSet<MixedFeedSilo>();
			}

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }

			public virtual ICollection<MixedFeedSilo> MixedFeedSiloSet { get; set; }
        }
	}
}