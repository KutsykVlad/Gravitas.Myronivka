namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base
{
    public class DeviceState<TInJson, TOutJson> : BaseDeviceState
    {
        public TInJson InData { get; set; }
        public TOutJson OutData { get; set; }
    }
}