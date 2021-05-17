using Gravitas.Model.Dto;

namespace Gravitas.Infrastructure.Platform.ApiClient.Devices
{
    public interface IDeviceSyncManager
    {
        BaseDeviceState GetDeviceState(long deviceId);
    }
}