using Gravitas.Model;
using Newtonsoft.Json;

namespace Gravitas.Model {

	public class ProductContent: BaseJsonConverter<ProductContent> {

		[JsonProperty("No")]
		public int No { get; set; }
		[JsonProperty("OrderNumber")]
		public string OrderNumber { get; set; }
		[JsonProperty("ProductID")]
		public string ProductId { get; set; }
		[JsonProperty("UnitID")]
		public string UnitId { get; set; }
		[JsonProperty("Quantity")]
		public double Quantity { get; set; }
	}
}
