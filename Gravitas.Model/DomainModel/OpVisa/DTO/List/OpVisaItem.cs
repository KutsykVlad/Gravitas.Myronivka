using System;

namespace Gravitas.Model.Dto {
	
	public class OpVisaItem : BaseEntity<long> {

		public DateTime? DateTime { get; set; }
		public string Message { get; set; }
		public string UserName { get; set; }
	}
}
