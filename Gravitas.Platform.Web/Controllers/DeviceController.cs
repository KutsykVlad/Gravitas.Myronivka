using System;
using System.Web.Mvc;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.Device;
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

        public ActionResult StateDialog(int deviceId, int nodeId)
        {
            var deviceType = _deviceWebManager.GetDeviceType(deviceId);
            if (deviceType == null) return null;

            ViewBag.LabDevResultTypeItems = _externalDataRepository.GetLabDevResultTypeItems();

            switch (deviceType.Value)
            {
                case DeviceType.LabBruker:
                case DeviceType.LabFoss:
                case DeviceType.LabFoss2:
                case DeviceType.LabInfrascan:
                    return PartialView("../Device/StateDialog/_LabAnalyserStateDialog",
                        _deviceWebManager.GetLabAnalyserStateDialogVm(deviceId, nodeId));
            }

            return null;
        }

        public ActionResult State(int deviceId)
        {
            var deviceType = _deviceWebManager.GetDeviceType(deviceId);
            if (deviceType == null) return null;

            ViewBag.LabDevResultTypeItems = _externalDataRepository.GetLabDevResultTypeItems();

            switch (deviceType.Value)
            {
                case DeviceType.LabBruker:
                    return PartialView("../Device/_LabBrukerState", _deviceWebManager.GetLabBrukerStateVm(deviceId));
                case DeviceType.LabFoss:
                    return PartialView("../Device/_LabFossState", _deviceWebManager.GetLabFossStateVm(deviceId));
                case DeviceType.LabInfrascan:
                    return PartialView("../Device/_LabInfrascanState", _deviceWebManager.GetLabInfrascanStateVm(deviceId));
            }

            return null;
        }

        public ActionResult ScaleState(int nodeId)
        {
            var routineData = _deviceWebManager.GetWeightbridgeStateVm(nodeId);
            return PartialView("../Device/_ScaleState", routineData);
        }

        public ActionResult DocNetValueDifference(int nodeId, double reduceValue)
        {
            var result = _deviceWebManager.GetWeightbridgeStateVm(nodeId);
            var vm = new DeviceStateVms.DocNetValueStateVm
            {
                CurrentDocNetValue = 0
            };
            if (result != null) vm.CurrentDocNetValue = Math.Abs(result.ScaleValue - reduceValue);
            return PartialView("../Device/_CurrentDocNetValue", vm);
        }

        public ActionResult LabFossState(int nodeId)
        {
            var routineData = _deviceWebManager.GetLabFossStateVm(nodeId);

            return PartialView("../Device/_LabFossState", routineData);
        }

        public ActionResult LabBrukerState(int nodeId)
        {
            var routineData = _deviceWebManager.GetLabBrukerStateVm(nodeId);

            return PartialView("../Device/_LabBrukerState", routineData);
        }

        public ActionResult LabInfrascanState(int nodeId)
        {
            var routineData = _deviceWebManager.GetLabInfrascanStateVm(nodeId);

            return PartialView("../Device/_LabInfrascanState", routineData);
        }
    }
}