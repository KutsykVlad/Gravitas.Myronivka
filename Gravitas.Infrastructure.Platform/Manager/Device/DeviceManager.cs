using System;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Model;
using Gravitas.Model.Dto;
using NLog;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Infrastructure.Platform.Manager.Device
{
    public class DeviceManager : IDeviceManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DeviceManager(IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpRoutineManager opRoutineManager)
        {
            _deviceRepository = deviceRepository;
            _nodeRepository = nodeRepository;
            _opRoutineManager = opRoutineManager;
        }

        public BaseDeviceState GetDeviceState(long nodeId, string deviceName)
        {
            var nodeDetail = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDetail == null)
                return null;

            if (nodeDetail.Config.DI.TryGetValue(deviceName, out var diConfig)
                && diConfig != null)
                return DeviceSyncManager.GetDeviceState(diConfig.DeviceId);
            if (nodeDetail.Config.DO.TryGetValue(deviceName, out var doConfig)
                && doConfig != null)
                return DeviceSyncManager.GetDeviceState(doConfig.DeviceId);
            if (nodeDetail.Config.Rfid.TryGetValue(deviceName, out var rfidConfig)
                && rfidConfig != null)
                return DeviceSyncManager.GetDeviceState(rfidConfig.DeviceId);
            if (nodeDetail.Config.Scale.TryGetValue(deviceName, out var scaleConfig)
                && scaleConfig != null)
                return DeviceSyncManager.GetDeviceState(scaleConfig.DeviceId);

            return null;
        }

        public void GetLoopState(Node nodeDto, out bool? incomingLoopState, out bool? outgoingLoopState, int timeout)
        {
            incomingLoopState = null;
            outgoingLoopState = null;

            if (nodeDto.Config == null) return;

            var iLoopLeftConfig = nodeDto.Config.DI.ContainsKey(Dom.Node.Config.DI.LoopIncoming)
                ? nodeDto.Config.DI[Dom.Node.Config.DI.LoopIncoming]
                : null;
            var iLoopRightConfig = nodeDto.Config.DI.ContainsKey(Dom.Node.Config.DI.LoopOutgoing)
                ? nodeDto.Config.DI[Dom.Node.Config.DI.LoopOutgoing]
                : null;

            DigitalInState iLoopLeftState = null;
            DigitalInState iLoopRightState = null;

            if (iLoopLeftConfig != null)
                iLoopLeftState = (DigitalInState) DeviceSyncManager.GetDeviceState(iLoopLeftConfig.DeviceId);

            if (iLoopRightConfig != null)
                iLoopRightState =
                    (DigitalInState) DeviceSyncManager.GetDeviceState(iLoopRightConfig.DeviceId);

            incomingLoopState = iLoopLeftState?.InData?.Value;
            if (iLoopLeftState?.LastUpdate == null
                || (DateTime.Now - iLoopLeftState.LastUpdate).Value.TotalSeconds > timeout)
                incomingLoopState = null;

            outgoingLoopState = iLoopRightState?.InData?.Value;
            if (iLoopRightState?.LastUpdate == null
                || (DateTime.Now - iLoopRightState.LastUpdate).Value.TotalSeconds > timeout)
                outgoingLoopState = null;
        }

        public ScaleState GetScaleState(Node nodeDto)
        {
            var scaleConfig = nodeDto.Config.Scale[Dom.Node.Config.Scale.Scale1];
            var scaleState = DeviceSyncManager.GetDeviceState(scaleConfig.DeviceId) as ScaleState;

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, scaleState))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return null;
            }

            if (scaleState == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error,
                    @"Помилка. Ваги не знайдено"));
                return null;
            }

            if (scaleState.InData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error,
                    @"Помилка. Стан ваг не визначено"));
                return null;
            }

            if (scaleState.InData.IsScaleError)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Warning,
                    @"Зауваження. Помилка на вагах"));
                return null;
            }

            return scaleState;
        }

        public void SetOutput(NodeConfig.DoConfig doConfig, bool value)
        {
            try
            {
                var oTrafficLightOutgoingState = (DigitalOutState) _deviceRepository.GetDeviceState(doConfig.DeviceId);
                if (oTrafficLightOutgoingState == null) return;
                DeviceSyncManager.SetDeviceOutData(oTrafficLightOutgoingState.Id, value);
            }
            catch (Exception e)
            {
                Logger.Fatal($"SetOutput: Error on setting output: {e.Message} : {e.StackTrace}");
            }
            
        }
    }
}