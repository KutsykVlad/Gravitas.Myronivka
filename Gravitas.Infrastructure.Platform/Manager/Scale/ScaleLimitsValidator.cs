using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class ScaleLimitsValidator
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IConnectManager _connectManager;
        private readonly GravitasDbContext _context;
        private readonly IPhoneInformTicketAssignmentRepository _phoneInformTicketAssignmentRepository;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        public ScaleLimitsValidator(IOpDataRepository opDataRepository,
            IRoutesInfrastructure routesInfrastructure,
            IConnectManager connectManager, 
            GravitasDbContext context, 
            IPhoneInformTicketAssignmentRepository phoneInformTicketAssignmentRepository)
        {
            _opDataRepository = opDataRepository;
            _routesInfrastructure = routesInfrastructure;
            _connectManager = connectManager;
            _context = context;
            _phoneInformTicketAssignmentRepository = phoneInformTicketAssignmentRepository;
        }
        
        public OnLoadScaleValidationDataModel GetLoadWeightValidationData(int ticketId)
        {
            var data = new OnLoadScaleValidationDataModel();

            var scaleOpDataGross = _context.ScaleOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId && x.TypeId == ScaleOpDataType.Gross)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            var scaleOpDataTare = _context.ScaleOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId && x.TypeId == ScaleOpDataType.Tare)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId);
            if (ticket.RouteTemplateId == null) return null;
            
            _logger.Trace($"GetLoadWeightValidationData: TruckWeightValue + TrailerWeightValue = {scaleOpDataGross?.TruckWeightValue ?? 0 + scaleOpDataGross?.TrailerWeightValue ?? 0}");

            data.WeightOnTruck = Math.Abs((scaleOpDataGross?.TruckWeightValue ?? 0 + scaleOpDataGross?.TrailerWeightValue ?? 0)
                                        - (scaleOpDataTare?.TruckWeightValue ?? 0 + scaleOpDataTare?.TrailerWeightValue ?? 0));
            
            _logger.Trace($"GetLoadWeightValidationData: WeightOnTruck = {data.WeightOnTruck}");

            var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(x => x.TicketId == ticketId);
            data.IsOverLoad = data.WeightOnTruck > singleWindowOpData.LoadTarget + singleWindowOpData.LoadTargetDeviationPlus;
            data.IsUnderLoad = data.WeightOnTruck < singleWindowOpData.LoadTarget - singleWindowOpData.LoadTargetDeviationMinus;
            
            _logger.Trace($"GetLoadWeightValidationData: IsOverLoad = {data.IsOverLoad}");
            _logger.Trace($"GetLoadWeightValidationData: IsUnderLoad = {data.IsUnderLoad}");

            data.WeightDifference = data.IsOverLoad
                ? data.WeightOnTruck - singleWindowOpData.LoadTarget + singleWindowOpData.LoadTargetDeviationPlus
                : singleWindowOpData.LoadTarget - singleWindowOpData.LoadTargetDeviationMinus - data.WeightOnTruck;

            _logger.Trace($"GetLoadWeightValidationData: WeightDifference = {data.WeightDifference}");
            
            return data;
        }
        
        public string IsWeightResultsValid(OnLoadScaleValidationDataModel scaleValidationDataModel, int ticketId)
        {
            _logger.Trace($"IsWeightResultsValid: IsOverLoad = {scaleValidationDataModel.IsOverLoad}");
            _logger.Trace($"IsWeightResultsValid: IsUnderLoad = {scaleValidationDataModel.IsUnderLoad}");
            if (!scaleValidationDataModel.IsOverLoad && !scaleValidationDataModel.IsUnderLoad) return string.Empty;
            
            var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticketId, null);
            scaleOpData.StateId = OpDataState.Canceled;
            _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);
    
            var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId);
            if (ticket.RouteTemplateId == null) throw new ArgumentException($"Ticket has no RouteTemplate = {ticket.Id}");
            
            _routesInfrastructure.SetSecondaryRoute(ticketId, scaleOpData.NodeId.Value,
                scaleValidationDataModel.IsOverLoad ? RouteType.PartUnload : RouteType.PartLoad);
            ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId);

            var isMixedFeedRoute = _routesInfrastructure.IsNodeAvailable((int) NodeIdValue.MixedFeedGuide, (int) (ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId));

            if (isMixedFeedRoute)
            {
                _routesInfrastructure.MoveForward(ticketId, (int) NodeIdValue.MixedFeedGuide);
            }
            else
            {
                if (_routesInfrastructure.IsRouteWithoutGuide(ticketId))
                {
                    _routesInfrastructure.AddDestinationOpData(ticketId, scaleOpData.NodeId.Value);
                }
                else
                {
                    var nextNode = _routesInfrastructure.GetNextNodes(ticketId).First();
                    _routesInfrastructure.MoveForward(ticketId, nextNode);
                }
                var card = _context.Cards.First(x => x.TicketContainerId == ticket.TicketContainerId);
                foreach (var item in _phoneInformTicketAssignmentRepository.GetAll().Where(e => e.TicketId == ticketId))
                {
                    _connectManager.SendSms(SmsTemplate.OnWeighBridgeRejected, ticketId, item.PhoneDictionary.PhoneNumber, cardId: card.Id);
                }
            }
                        
            return "?????????? ???????????????? ???????????? ???? ????????????????. ???????????????? ?????? ????????????????????????.";
        }
        
        public double? GetPartLoadUnloadValue(int ticketId)
        {
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);
            var lastScaleGross = _context.ScaleOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId && x.TypeId == ScaleOpDataType.Gross)
                .OrderBy(x => x.CheckInDateTime)
                .FirstOrDefault();
            var lastScaleTare = _context.ScaleOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId && x.TypeId == ScaleOpDataType.Tare)
                .OrderBy(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (lastScaleGross != null && lastScaleTare != null)
            {
                var scale = Math.Abs((lastScaleGross.TruckWeightValue ?? 0 + lastScaleGross.TrailerWeightValue ?? 0)
                                     - (lastScaleTare.TruckWeightValue ?? 0 + lastScaleTare.TrailerWeightValue ?? 0));
                var minScale = singleWindowOpData.LoadTarget - singleWindowOpData.LoadTargetDeviationMinus;
                var maxScale = singleWindowOpData.LoadTarget + singleWindowOpData.LoadTargetDeviationPlus;

                if (scale < minScale) return minScale - scale;

                if (scale > maxScale) return scale - maxScale;
            }
            else
            {
                return null;
            }

            return null;
        }
    }
}