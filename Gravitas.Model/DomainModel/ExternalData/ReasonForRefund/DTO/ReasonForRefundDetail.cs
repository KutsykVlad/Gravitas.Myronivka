namespace Gravitas.Model.Dto {

	public static partial class ExternalData {
		public class ReasonForRefundDetail : BaseEntity<string> {

			public string Code { get; set; }
			public string Name { get; set; }
		}
	}

}