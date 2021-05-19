using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.Device
{
    public interface IDeviceWebManager
    {
        DeviceType? GetDeviceType(int deviceId);

        DeviceStateVms.WeighbridgeStateVm GetWeightbridgeStateVm(int nodeId);

        DeviceStateVms.LabFossStateVm GetLabFossStateVm(int deviceId);
        DeviceStateVms.LabBrukerStateVm GetLabBrukerStateVm(int deviceId);

        DeviceStateVms.LabAnalyserStateDialogVm GetLabAnalyserStateDialogVm(int deviceId, int nodeId);
        DeviceStateVms.LabInfroscanStateVm GetLabInfrascanStateVm(int deviceId);
    }
}