using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.Dto;

namespace Gravitas.Core.DeviceManager.Device
{
    public interface IDeviceManager
    {
        BaseDeviceState GetDeviceState(int nodeId, string deviceName);
        void GetLoopState(Node nodeDto, out bool? incomingLoopState, out bool? outgoingLoopState, int timeout);
        ScaleState GetScaleState(Node nodeDto);
        void SetOutput(NodeConfig.DoConfig doConfig, bool value);
    }
}