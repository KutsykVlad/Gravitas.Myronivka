using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Device.DAO
{
    public class DeviceState : BaseEntity<int>
    {
        public DeviceState()
        {
            DeviceSet = new HashSet<Device>();
        }

        public DateTime? LastUpdate { get; set; }
        public int ErrorCode { get; set; }
        public string InData { get; set; }
        public string OutData { get; set; }

        public virtual ICollection<Device> DeviceSet { get; set; }
    }
}