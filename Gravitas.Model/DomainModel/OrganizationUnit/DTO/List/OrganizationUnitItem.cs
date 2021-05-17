namespace Gravitas.Model.Dto {

	public class OrganizationUnitItem : BaseEntity<long> {

		public string Name { get; set; }
		public string TypeName { get; set; }
		public int ChildCount { get; set; }
		public int NodeCount { get; set; }
	}
}
