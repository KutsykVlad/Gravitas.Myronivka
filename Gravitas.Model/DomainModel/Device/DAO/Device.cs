using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model {

	public partial class Device : BaseEntity<long> {
		
		public Device() {
			ChildDeviceSet = new HashSet<Device>();
			CameraImageSet = new HashSet<OpCameraImage>();
		}

	public long? ParentDeviceId { get; set; }

		public bool IsActive { get; set; }
		public long TypeId { get; set; }

		public long? ParamId { get; set; }
		public long? StateId { get; set; }

		public string Name { get; set; }

		//Navigation properties
		public virtual Device ParentDevice { get; set; }
		public virtual ICollection<Device> ChildDeviceSet { get; set; }
		public virtual DeviceType DeviceType { get; set; }
		public virtual DeviceParam DeviceParam { get; set; }
		public virtual DeviceState  DeviceState { get; set; }
		public virtual ICollection<OpCameraImage> CameraImageSet { get; set; }

	}
}
