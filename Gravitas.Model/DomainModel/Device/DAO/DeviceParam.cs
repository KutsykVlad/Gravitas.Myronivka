using System.Collections.Generic;

namespace Gravitas.Model {

	public class DeviceParam : BaseEntity<long> {

		public DeviceParam() {
			DeviceSet = new HashSet<Device>();
		}

		public string ParamJson { get; set; }
		
		//Navigation properties
		public virtual ICollection<Device> DeviceSet { get; set; }
	}
}
