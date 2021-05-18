using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class ScaleStateValidator
    {
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public ScaleStateValidator(IOpRoutineManager opRoutineManager)
        {
            _opRoutineManager = opRoutineManager;
        }

        public bool IsScaleStateOk(ScaleState scaleState, int nodeId)
        {
            if (!scaleState.InData.IsImmobile)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(Model.Node.ProcessingMsg.Type.Warning,@"Вага не стабільна."));
                return false;
            }

            if (scaleState.InData.Value < GlobalConfigurationManager.ScaleMinLoad)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(Model.Node.ProcessingMsg.Type.Info,@"Вага надто мала."));
                return false;
            }

            return true;
        }
    }
}