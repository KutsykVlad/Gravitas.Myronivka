using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;

namespace Gravitas.Core.Manager.ScaleMettlerPT6S3
{
    public interface IScaleMettlerPT6S3Manager
    {
        ScaleInJsonState GetState(int deviceId);
    }
}