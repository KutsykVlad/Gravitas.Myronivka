using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Device.DAO
{
    public class Device : BaseEntity<int>
    {
        public Device()
        {
            ChildDeviceSet = new HashSet<Device>();
            CameraImageSet = new HashSet<OpCameraImage.OpCameraImage>();
        }

        public int? ParentDeviceId { get; set; }

        public bool IsActive { get; set; }
        public DeviceType TypeId { get; set; }
        public int DeviceParamId { get; set; }
        public int DeviceStateId { get; set; }
        public string Name { get; set; }

        public virtual Device ParentDevice { get; set; }
        public virtual ICollection<Device> ChildDeviceSet { get; set; }
        public virtual DeviceParam DeviceParam { get; set; }
        public virtual DeviceState DeviceState { get; set; }
        public virtual ICollection<OpCameraImage.OpCameraImage> CameraImageSet { get; set; }
    }
}