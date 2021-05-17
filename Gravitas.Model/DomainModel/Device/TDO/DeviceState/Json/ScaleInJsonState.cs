namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json
{
    public class ScaleInJsonState
    {
        public double Value { get; set; }
        public bool IsZero { get; set; }
        public bool IsImmobile { get; set; }
        public bool IsGross { get; set; }
        public bool IsScaleError { get; set; }
    }
}