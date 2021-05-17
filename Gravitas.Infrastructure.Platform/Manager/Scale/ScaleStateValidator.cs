using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using NLog;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class ScaleStateValidator
    {
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IOpDataManager _opDataManager;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public ScaleStateValidator(IOpRoutineManager opRoutineManager,
            IOpDataRepository opDataRepository,
            IOpDataManager opDataManager,
            GravitasDbContext context)
        {
            _opRoutineManager = opRoutineManager;
            _opDataRepository = opDataRepository;
            _opDataManager = opDataManager;
            _context = context;
        }

        public bool IsScaleStateOk(ScaleState scaleState, long nodeId)
        {
            if (!scaleState.InData.IsImmobile)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,@"Вага не стабільна."));
                return false;
            }

            if (scaleState.InData.Value < GlobalConfigurationManager.ScaleMinLoad)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Info,@"Вага надто мала."));
                return false;
            }

            return true;
        }
    }
}