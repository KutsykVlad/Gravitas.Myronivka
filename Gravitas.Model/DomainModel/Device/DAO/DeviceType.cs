using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model {

	public partial class DeviceType : BaseEntity<long> {

		public DeviceType() {
			DeviceSet = new HashSet<Device>();
		}
		
		public string Name { get; set; }

		// Navigation Properties
		public virtual ICollection<Device> DeviceSet { get; set; }
	}
}
