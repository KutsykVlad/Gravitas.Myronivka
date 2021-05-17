namespace Gravitas.Model.Dto {

	public static partial class ExternalData {

		public class BudgetItem : BaseEntity<string> {

			public string Code { get; set; }
			public string Name { get; set; }
            public bool IsFolder { get; set; }
		}
	}

}
