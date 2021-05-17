using System;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceWebManager _deviceWebManager;
        private readonly IExternalDataRepository _externalDataRepository;

        public DeviceController(IDeviceWebManager deviceWebManager, IExternalDataRepository externalDataRepository)
        {
            _deviceWebManager = deviceWebManager;
            _externalDataRepository = externalDataRepository;
        }

        public ActionResult StateDialog(long deviceId, long nodeId)
        {
            var deviceType = _deviceWebManager.GetDeviceType(deviceId);
            if (deviceType == null) return null;

            ViewBag.LabDevResultTypeItems = _externalDataRepository.GetLabDevResultTypeItems();

            switch (deviceType.Value)
            {
                case Dom.Device.Type.LabBruker:
                case Dom.Device.Type.LabFoss:
                case Dom.Device.Type.LabFoss2:
                case Dom.Device.Type.LabInfrascan:
                    return PartialView("../Device/StateDialog/_LabAnalyserStateDialog",
                        _deviceWebManager.GetLabAnalyserStateDialogVm(deviceId, nodeId));
            }

            return null;
        }

        public ActionResult State(long deviceId)
        {
            var deviceType = _deviceWebManager.GetDeviceType(deviceId);
            if (deviceType == null) return null;

            ViewBag.LabDevResultTypeItems = _externalDataRepository.GetLabDevResultTypeItems();

            switch (deviceType.Value)
            {
                case Dom.Device.Type.LabBruker:
                    return PartialView("../Device/_LabBrukerState", _deviceWebManager.GetLabBrukerStateVm(deviceId));
                case Dom.Device.Type.LabFoss:
                    return PartialView("../Device/_LabFossState", _deviceWebManager.GetLabFossStateVm(deviceId));
                case Dom.Device.Type.LabInfrascan:
                    return PartialView("../Device/_LabInfrascanState", _deviceWebManager.GetLabInfrascanStateVm(deviceId));
            }

            return null;
        }

        public ActionResult ScaleState(long nodeId)
        {
            var routineData = _deviceWebManager.GetWeightbridgeStateVm(nodeId);
            return PartialView("../Device/_ScaleState", routineData);
        }

        public ActionResult DocNetValueDifference(long nodeId, double reduceValue)
        {
            var result = _deviceWebManager.GetWeightbridgeStateVm(nodeId);
            var vm = new DeviceStateVms.DocNetValueStateVm
            {
                CurrentDocNetValue = 0
            };
            if (result != null) vm.CurrentDocNetValue = Math.Abs(result.ScaleValue - reduceValue);
            return PartialView("../Device/_CurrentDocNetValue", vm);
        }

        public ActionResult LabFossState(long nodeId)
        {
            var routineData = _deviceWebManager.GetLabFossStateVm(nodeId);

            return PartialView("../Device/_LabFossState", routineData);
        }

        public ActionResult LabBrukerState(long nodeId)
        {
            var routineData = _deviceWebManager.GetLabBrukerStateVm(nodeId);

            return PartialView("../Device/_LabBrukerState", routineData);
        }

        public ActionResult LabInfrascanState(long nodeId)
        {
            var routineData = _deviceWebManager.GetLabInfrascanStateVm(nodeId);

            return PartialView("../Device/_LabInfrascanState", routineData);
        }
    }
}