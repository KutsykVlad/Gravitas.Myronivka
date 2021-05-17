using System.Collections.Generic;
using Gravitas.Model;

namespace Gravitas.Model.Dto {

	public class ProductContentList : BaseJsonConverter<ProductContentList> {

		public ICollection<ProductContentItem> Items { get; set; }
		public int Count { get; set; }
	}
}
