using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.Dto;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class ScaleManager : IScaleManager
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly IOpDataManager _opDataManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IConnectManager _connectManager;
        private readonly IPhonesRepository _phonesRepository;
        private readonly GravitasDbContext _context;
        private readonly IPhoneInformTicketAssignmentRepository _phoneInformTicketAssignmentRepository;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ScaleManager(IOpDataRepository opDataRepository,
            IOpRoutineManager opRoutineManager,
            IOpDataManager opDataManager,
            IRoutesInfrastructure routesInfrastructure, 
            IConnectManager connectManager,
            IPhonesRepository phonesRepository,
            GravitasDbContext context, 
            IPhoneInformTicketAssignmentRepository phoneInformTicketAssignmentRepository)
        {
            _opDataRepository = opDataRepository;
            _opRoutineManager = opRoutineManager;
            _opDataManager = opDataManager;
            _routesInfrastructure = routesInfrastructure;
            _connectManager = connectManager;
            _phonesRepository = phonesRepository;
            _context = context;
            _phoneInformTicketAssignmentRepository = phoneInformTicketAssignmentRepository;
        }

        public bool IsScaleStateOk(ScaleState scaleState, long nodeId)
        {
            var stateValidator = new ScaleStateValidator(_opRoutineManager, _opDataRepository, _opDataManager, _context);
            return stateValidator.IsScaleStateOk(scaleState, nodeId);
        }

        public double? GetPartLoadUnloadValue(long ticketId)
        {
            var limitsValidator = new ScaleLimitsValidator(_opDataRepository, _routesInfrastructure, _connectManager, _phonesRepository, _context, _phoneInformTicketAssignmentRepository);
            return limitsValidator.GetPartLoadUnloadValue(ticketId);
        }

        public OnLoadScaleValidationDataModel GetLoadWeightValidationData(long ticketId)
        {
            var limitsValidator = new ScaleLimitsValidator(_opDataRepository, _routesInfrastructure, _connectManager, _phonesRepository, _context, _phoneInformTicketAssignmentRepository);
            return limitsValidator.GetLoadWeightValidationData(ticketId);
        }

        public bool IsTareMoreGross(Node nodeDto, bool isTruckWeighting, ScaleOpData scaleOpData)
        {
            if (scaleOpData.TypeId != Dom.ScaleOpData.Type.Tare) return false;
            var previousScaleData = _context.ScaleOpDatas
                .Where(x =>
                    x.TicketId == nodeDto.Context.TicketId 
                    && x.StateId == Dom.OpDataState.Processed
                    && x.TypeId == Dom.ScaleOpData.Type.Gross)
                .OrderByDescending(t => t.CheckOutDateTime)
                .FirstOrDefault();
            
            if (previousScaleData != null && (previousScaleData.TrailerIsAvailable == false && isTruckWeighting || !isTruckWeighting))
            {
                _logger.Debug($"IsTareMoreGross: isTruckWeighting = {isTruckWeighting}");
                var scaleConfig = nodeDto.Config.Scale[Dom.Node.Config.Scale.Scale1];
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
       
        public string IsWeightResultsValid(OnLoadScaleValidationDataModel scaleValidationDataModel, long ticketId)
        {
            var limitsValidator = new ScaleLimitsValidator(_opDataRepository, _routesInfrastructure, _connectManager, _phonesRepository, _context, _phoneInformTicketAssignmentRepository);
            return limitsValidator.IsWeightResultsValid(scaleValidationDataModel, ticketId);
        }
    }
}