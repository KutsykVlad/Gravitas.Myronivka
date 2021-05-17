using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public interface IDeviceWebManager
    {
        long? GetDeviceType(long deviceId);

        DeviceStateVms.WeighbridgeStateVm GetWeightbridgeStateVm(long nodeId);

        DeviceStateVms.LabFossStateVm GetLabFossStateVm(long deviceId);
        DeviceStateVms.LabBrukerStateVm GetLabBrukerStateVm(long deviceId);

        DeviceStateVms.LabAnalyserStateDialogVm GetLabAnalyserStateDialogVm(long deviceId, long nodeId);
        DeviceStateVms.LabInfroscanStateVm GetLabInfrascanStateVm(long deviceId);
    }
}