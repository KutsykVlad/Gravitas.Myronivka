using System;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Base;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using NLog;

namespace Gravitas.Core.DeviceManager.Device
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

        public BaseDeviceState GetDeviceState(int nodeId, string deviceName)
        {
            var nodeDetail = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDetail == null)
                return null;

            if (nodeDetail.Config.DI.TryGetValue(deviceName, out var diConfig)
                && diConfig != null)
                return Program.GetDeviceState(diConfig.DeviceId);
            if (nodeDetail.Config.DO.TryGetValue(deviceName, out var doConfig)
                && doConfig != null)
                return Program.GetDeviceState(doConfig.DeviceId);
            if (nodeDetail.Config.Rfid.TryGetValue(deviceName, out var rfidConfig)
                && rfidConfig != null)
                return Program.GetDeviceState(rfidConfig.DeviceId);
            if (nodeDetail.Config.Scale.TryGetValue(deviceName, out var scaleConfig)
                && scaleConfig != null)
                return Program.GetDeviceState(scaleConfig.DeviceId);

            return null;
        }

        public void GetLoopState(NodeDetails nodeDetailsDto, out bool? incomingLoopState, out bool? outgoingLoopState, int timeout)
        {
            incomingLoopState = null;
            outgoingLoopState = null;

            if (nodeDetailsDto.Config == null) return;

            var iLoopLeftConfig = nodeDetailsDto.Config.DI.ContainsKey(NodeData.Config.DI.LoopIncoming)
                ? nodeDetailsDto.Config.DI[NodeData.Config.DI.LoopIncoming]
                : null;
            var iLoopRightConfig = nodeDetailsDto.Config.DI.ContainsKey(NodeData.Config.DI.LoopOutgoing)
                ? nodeDetailsDto.Config.DI[NodeData.Config.DI.LoopOutgoing]
                : null;

            DigitalInState iLoopLeftState = null;
            DigitalInState iLoopRightState = null;

            if (iLoopLeftConfig != null)
                iLoopLeftState = (DigitalInState) Program.GetDeviceState(iLoopLeftConfig.DeviceId);

            if (iLoopRightConfig != null)
                iLoopRightState =
                    (DigitalInState) Program.GetDeviceState(iLoopRightConfig.DeviceId);

            incomingLoopState = iLoopLeftState?.InData?.Value;
            if (iLoopLeftState?.LastUpdate == null
                || (DateTime.Now - iLoopLeftState.LastUpdate).Value.TotalSeconds > timeout)
                incomingLoopState = null;

            outgoingLoopState = iLoopRightState?.InData?.Value;
            if (iLoopRightState?.LastUpdate == null
                || (DateTime.Now - iLoopRightState.LastUpdate).Value.TotalSeconds > timeout)
                outgoingLoopState = null;
        }

        public ScaleState GetScaleState(NodeDetails nodeDetailsDto)
        {
            var scaleConfig = nodeDetailsDto.Config.Scale[NodeData.Config.Scale.Scale1];
            var scaleState = Program.GetDeviceState(scaleConfig.DeviceId) as ScaleState;

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, scaleState))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return null;
            }

            if (scaleState == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error,
                    @"Помилка. Ваги не знайдено"));
                return null;
            }

            if (scaleState.InData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error,
                    @"Помилка. Стан ваг не визначено"));
                return null;
            }

            if (scaleState.InData.IsScaleError)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Warning,
                    @"Зауваження. Помилка на вагах"));
                return null;
            }

            return scaleState;
        }

        public void SetOutput(NodeConfig.DoConfig doConfig, bool value)
        {
            try
            {
                var oTrafficLightOutgoingState = (DigitalOutState) Program.GetDeviceState(doConfig.DeviceId);
                if (oTrafficLightOutgoingState == null) return;
                Program.SetDeviceOutData(oTrafficLightOutgoingState.Id, value);
            }
            catch (Exception e)
            {
                Logger.Fatal($"SetOutput: Error on setting output: {e.Message} : {e.StackTrace}");
            }
            
        }
    }
}