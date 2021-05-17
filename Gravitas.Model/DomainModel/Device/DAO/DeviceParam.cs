using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Device.DAO
{
    public class DeviceParam : BaseEntity<int>
    {
        public DeviceParam()
        {
            DeviceSet = new HashSet<Device>();
        }

        public string ParamJson { get; set; }

        public virtual ICollection<Device> DeviceSet { get; set; }
    }
}