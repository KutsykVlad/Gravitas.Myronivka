namespace Gravitas.Model.Dto {

	public class DeviceState<TInJson, TOutJson> : BaseDeviceState
	{
		public TInJson InData { get; set; }
		public TOutJson OutData { get; set; }
	}

}
