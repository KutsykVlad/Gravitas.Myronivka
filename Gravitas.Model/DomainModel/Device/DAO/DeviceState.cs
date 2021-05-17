using System;
using System.Collections.Generic;

namespace Gravitas.Model {

	public class DeviceState : BaseEntity<long> {

		public DeviceState() {
			DeviceSet = new HashSet<Device>();
		}

		public DateTime? LastUpdate { get; set; }
		public int ErrorCode { get; set; }

		public string InData { get; set; }
		public string OutData { get; set; }

		//Navigation Properties
		public virtual ICollection<Device> DeviceSet{ get; set; }
	}
}
