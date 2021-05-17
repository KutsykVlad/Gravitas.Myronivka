using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Device.DAO
{
    public class DeviceType : BaseEntity<int>
    {
        public DeviceType()
        {
            DeviceSet = new HashSet<Device>();
        }

        public string Name { get; set; }

        // Navigation Properties
        public virtual ICollection<Device> DeviceSet { get; set; }
    }
}