using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class ScaleManager : IScaleManager
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IConnectManager _connectManager;
        private readonly GravitasDbContext _context;
        private readonly IPhoneInformTicketAssignmentRepository _phoneInformTicketAssignmentRepository;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ScaleManager(IOpDataRepository opDataRepository,
            IOpRoutineManager opRoutineManager,
            IRoutesInfrastructure routesInfrastructure, 
            IConnectManager connectManager,
            GravitasDbContext context, 
            IPhoneInformTicketAssignmentRepository phoneInformTicketAssignmentRepository)
        {
            _opDataRepository = opDataRepository;
            _opRoutineManager = opRoutineManager;
            _routesInfrastructure = routesInfrastructure;
            _connectManager = connectManager;
            _context = context;
            _phoneInformTicketAssignmentRepository = phoneInformTicketAssignmentRepository;
        }

        public bool IsScaleStateOk(ScaleState scaleState, int nodeId)
        {
            var stateValidator = new ScaleStateValidator(_opRoutineManager);
            return stateValidator.IsScaleStateOk(scaleState, nodeId);
        }

        public double? GetPartLoadUnloadValue(int ticketId)
        {
            var limitsValidator = new ScaleLimitsValidator(_opDataRepository, _routesInfrastructure, _connectManager, _context, _phoneInformTicketAssignmentRepository);
            return limitsValidator.GetPartLoadUnloadValue(ticketId);
        }

        public OnLoadScaleValidationDataModel GetLoadWeightValidationData(int ticketId)
        {
            var limitsValidator = new ScaleLimitsValidator(_opDataRepository, _routesInfrastructure, _connectManager, _context, _phoneInformTicketAssignmentRepository);
            return limitsValidator.GetLoadWeightValidationData(ticketId);
        }

        public bool IsTareMoreGross(Model.DomainModel.Node.TDO.Detail.Node nodeDto, bool isTruckWeighting, ScaleOpData scaleOpData)
        {
            if (scaleOpData.TypeId != ScaleOpDataType.Tare) return false;
            var previousScaleData = _context.ScaleOpDatas
                .Where(x =>
                    x.TicketId == nodeDto.Context.TicketId 
                    && x.StateId == OpDataState.Processed
                    && x.TypeId == ScaleOpDataType.Gross)
                .OrderByDescending(t => t.CheckOutDateTime)
                .FirstOrDefault();
            
            if (previousScaleData != null && (previousScaleData.TrailerIsAvailable == false && isTruckWeighting || !isTruckWeighting))
            {
                _logger.Debug($"IsTareMoreGross: isTruckWeighting = {isTruckWeighting}");
                var scaleConfig = nodeDto.Config.Scale[Model.Node.Config.Scale.Scale1];
                var scaleState = DeviceSyncManager.GetDeviceState(scaleConfig.DeviceId) as ScaleState;
                if (scaleState == null) throw new Exception("IsTareMoreGross: Scale scale is invalid");

                var tareWeight = scaleState.InData.Value + (scaleOpData.TruckWeightValue ?? 0);
                var grossWeight = (previousScaleData.TruckWeightValue ?? 0) + (previousScaleData.TrailerWeightValue ?? 0);

                if (tareWeight - grossWeight > 100)
                {
                    _logger.Error($"IsTareMoreGross: tareWeight = {tareWeight}; grossWeight = {grossWeight}");
                    return true;
                }
            }

            return false;
        }
       
        public string IsWeightResultsValid(OnLoadScaleValidationDataModel scaleValidationDataModel, int ticketId)
        {
            var limitsValidator = new ScaleLimitsValidator(_opDataRepository, _routesInfrastructure, _connectManager, _context, _phoneInformTicketAssignmentRepository);
            return limitsValidator.IsWeightResultsValid(scaleValidationDataModel, ticketId);
        }
    }
}