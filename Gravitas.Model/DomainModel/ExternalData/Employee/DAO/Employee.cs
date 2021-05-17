using System.Collections.Generic;

namespace Gravitas.Model {

	public static partial class ExternalData {

		public class Employee : BaseEntity<string> {

		    public Employee()
		    {
		        CardSet = new HashSet<Card>();
                OpVisaSet = new HashSet<OpVisa>();
                EmployeeRoles = new HashSet<EmployeeRole>();
            }

			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public string Position { get; set; }
			public string Email { get; set; }
			public string PhoneNo { get; set; }
			public bool IsFolder { get; set; }
			public string ParentId { get; set; }

		    public virtual ICollection<Card> CardSet { get; set; }
            public virtual ICollection<OpVisa> OpVisaSet { get; set; }
            public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        }
	}
}