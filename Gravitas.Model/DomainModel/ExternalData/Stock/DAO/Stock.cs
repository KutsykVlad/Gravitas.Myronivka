namespace Gravitas.Model {

	public static partial class ExternalData {
		public class Stock : BaseEntity<string> {

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public string Address { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }
			public string CustomerId { get; set; }
		}
	}

}