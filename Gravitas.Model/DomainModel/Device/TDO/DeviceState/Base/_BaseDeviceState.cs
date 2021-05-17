using System;

namespace Gravitas.Model.Dto {

	public class BaseDeviceState : BaseEntity<long> { 
		public DateTime? LastUpdate { get; set; }
		public int ErrorCode { get; set; }
	}
}

