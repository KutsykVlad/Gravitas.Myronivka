using System;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Infrastructure.Platform.ApiClient
{
    public class OneCApiService : IOneCApiService
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly OneCApiClient _client;
        private readonly IOpDataRepository _opDataRepository;
        private readonly GravitasDbContext _context;
        private readonly IOpDataManager _opDataManager;

        public OneCApiService(OneCApiClient client, 
            IOpDataRepository opDataRepository,
            GravitasDbContext context, 
            IOpDataManager opDataManager)
        {
            _client = client;
            _opDataRepository = opDataRepository;
            _context = context;
            _opDataManager = opDataManager;
        }

        public void UpdateOneCData(Guid singleWindowOpDataId, bool closeDeliveryBill)
        {
            var singleWindowOpData = _context.SingleWindowOpDatas.First(x => x.Id == singleWindowOpDataId);
            var createData = _opDataRepository.GetLastProcessed<ScaleOpData>(x => x.TicketId == singleWindowOpData.TicketId && x.TypeId == ScaleOpDataType.Gross)?
                .CheckOutDateTime;
            var request = new OneCApiClient.UpdateDeliveryBillDto.Request
            {
                Activity = closeDeliveryBill ? 1 : 0,
                Id = singleWindowOpData.DeliveryBillId,
                CreateOperatorId = singleWindowOpData.CreateOperatorId,
                CreateDate = createData ?? singleWindowOpData.CreateDate,
                EditDate = singleWindowOpData.EditDate,
                RegistrationTime = singleWindowOpData.RegistrationDateTime,
                InTime = singleWindowOpData.InTime,
                OutTime = singleWindowOpData.OutTime,
                FirstGrossTime = singleWindowOpData.FirstGrossTime,
                FirstTareTime = singleWindowOpData.FirstTareTime,
                LastGrossTime = singleWindowOpData.LastGrossTime,
                LastTareTime = singleWindowOpData.LastTareTime,
                EditOperatorId = singleWindowOpData.EditOperatorId,
                GrossValue = singleWindowOpData.GrossValue ?? 0,
                TareValue = singleWindowOpData.TareValue ?? 0,
                NetValue = (singleWindowOpData.NetValue ?? 0) - (singleWindowOpData.PackingWeightValue ?? 0),
                DriverOneId = singleWindowOpData.DriverOneId,
                DriverTwoId = singleWindowOpData.DriverTwoId,
                TransportId = singleWindowOpData.TransportId,
                HiredDriverCode = singleWindowOpData.HiredDriverCode,
                HiredTansportNumber = singleWindowOpData.HiredTransportNumber,
                IncomeInvoiceSeries = singleWindowOpData.IncomeInvoiceSeries,
                IncomeInvoiceNumber = singleWindowOpData.IncomeInvoiceNumber,
                ReceiverStockId = singleWindowOpData.ReceiverDepotId,
                IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier ? "1" : "0",
                CarrierCode = string.IsNullOrWhiteSpace(singleWindowOpData.CarrierCode) 
                    ? singleWindowOpData.CustomPartnerName 
                    : singleWindowOpData.CarrierId.ToString(),
                BuyBudgetsId = singleWindowOpData.BuyBudgetId,
                SellBudgetsId = singleWindowOpData.SellBudgetId,
                PackingWeightValue = singleWindowOpData.PackingWeightValue ?? 0,
                SupplyCode = singleWindowOpData.SupplyCode,
                CollectionPointId = singleWindowOpData.CollectionPointId,
                LabHumidityType = singleWindowOpData.LabHumidityName,
                LabImpurityType = singleWindowOpData.LabImpurityName,
                LabIsInfectioned = singleWindowOpData.LabIsInfectioned ? "1" : "0",
                LabHumidityValue = singleWindowOpData.LabHumidityValue ?? 0,
                LabImpurityValue = singleWindowOpData.LabImpurityValue ?? 0,
                DocHumidityValue = singleWindowOpData.DocHumidityValue ?? 0,
                DocImpurityValue = singleWindowOpData.DocImpurityValue ?? 0,
                DocNetValue = singleWindowOpData.DocumentTypeId == ExternalData.DeliveryBill.Type.Outgoing 
                    ? (singleWindowOpData.NetValue ?? 0) - (singleWindowOpData.PackingWeightValue ?? 0) 
                    : singleWindowOpData.DocNetValue ?? 0,
                DocNetDateTime = singleWindowOpData.DocNetDateTime,
                ReturnCauseId = singleWindowOpData.ReturnCauseId,
                TrailerId = singleWindowOpData.TrailerId,
                TrailerNumber = singleWindowOpData.HiredTrailerNumber,
                TripTicketNumber = singleWindowOpData.TripTicketNumber,
                TripTicketDateTime = singleWindowOpData.TripTicketDateTime,
                WarrantSeries = singleWindowOpData.WarrantSeries,
                WarrantNumber = singleWindowOpData.WarrantNumber,
                WarrantDateTime = singleWindowOpData.WarrantDateTime,
                WarrantManagerName = singleWindowOpData.WarrantManagerName,
                StampList = singleWindowOpData.StampList,
                RuleNumber = singleWindowOpData.RuleNumber,
                TrailerGrossValue = singleWindowOpData.TrailerGrossValue ?? 0,
                TrailerTareValue = singleWindowOpData.TrailerTareValue ?? 0,
                IncomeDocGrossValue = singleWindowOpData.IncomeDocGrossValue ?? 0,
                IncomeDocTareValue = singleWindowOpData.IncomeDocTareValue ?? 0,
                IncomeDocDateTime = singleWindowOpData.IncomeDocDateTime,
                Comments = $"{singleWindowOpData.Comments} {_opDataRepository.GetLastProcessed<LabFacelessOpData>(singleWindowOpData.TicketId)?.Comment}",
                WeightDeltaValue = singleWindowOpData.WeightDeltaValue ?? 0,
                SupplyType = singleWindowOpData.SupplyTransportTypeId,
                LabolatoryOperatorId = singleWindowOpData.LabolatoryOperatorId,
                GrossOperatorId = string.Empty,
                ScaleInNumber = singleWindowOpData.ScaleInNumber,
                ScaleOutNumber = singleWindowOpData.ScaleOutNumber,
                BatchNumber = singleWindowOpData.BatchNumber,
                TareOperatorId = string.Empty,
                LoadingOperatorId = singleWindowOpData.LoadingOperatorId,
                LoadOutDate = singleWindowOpData.LoadOutDateTime,
                CarrierRouteId = singleWindowOpData.CarrierRouteId,
                LabOilContentValue = singleWindowOpData.LabOilContentValue ?? 0,
                InformationCarrier = singleWindowOpData.InformationCarrier,
                WeightingStatistics = _opDataManager.GetEvents(singleWindowOpData.TicketId.Value, (int?) OpDataEventType.Weight)
            };
            
            Logger.Info(JsonConvert.SerializeObject(request));

            if (!closeDeliveryBill)
            {
                // update in background
                Task.Run(() =>
                {
                    _client.PostUpdateDeliveryBill(request, 200);
                }); 
            }
            else
            {
                _client.PostUpdateDeliveryBill(request, 200);
            }
        }
    }
}