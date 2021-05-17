using System.Collections.Generic;

namespace Gravitas.Model {

	public static partial class ExternalData {
		public class ExternalUser : BaseEntity<string> {

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public string EmployeeId { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }

			
		}
	}
}