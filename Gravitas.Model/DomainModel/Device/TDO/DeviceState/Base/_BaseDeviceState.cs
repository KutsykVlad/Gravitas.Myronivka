using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base
{
    public class BaseDeviceState : BaseEntity<int>
    {
        public DateTime? LastUpdate { get; set; }
        public int ErrorCode { get; set; }
    }
}