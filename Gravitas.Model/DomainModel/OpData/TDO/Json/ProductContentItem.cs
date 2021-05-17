using Gravitas.Model;
using Newtonsoft.Json;

namespace Gravitas.Model.Dto {

	public class ProductContentItem : BaseJsonConverter<ProductContentItem> {

		public int No { get; set; }
		public string OrderNumber { get; set; }
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string UnitId { get; set; }
		public string UnitName { get; set; }
		public double Quantity { get; set; }
	}
}
