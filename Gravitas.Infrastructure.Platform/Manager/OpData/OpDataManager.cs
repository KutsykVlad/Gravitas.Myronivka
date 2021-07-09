using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Extension;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpData.DAO.Json;
using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainModel.OpData.TDO.Json;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OpDataEvent.DTO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.TDO.Detail.LabFacelessOpData;
using LabFacelessOpDataComponent = Gravitas.Model.DomainModel.OpData.TDO.Detail.LabFacelessOpDataComponent;
using SingleWindowOpData = Gravitas.Model.DomainModel.OpData.DAO.SingleWindowOpData;

namespace Gravitas.Infrastructure.Platform.Manager.OpData
{
    public class OpDataManager : IOpDataManager
    {
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly GravitasDbContext _context;

        public OpDataManager(
            IOpDataRepository opDataRepository,
            IExternalDataRepository externalDataRepository,
            ITicketRepository ticketRepository, 
            GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _externalDataRepository = externalDataRepository;
            _ticketRepository = ticketRepository;
            _context = context;
        }

        public void AddEvent(OpDataEvent opDataEvent)
        {
            _opDataRepository.Add<OpDataEvent, int>(opDataEvent);
        }

        public OpDataEventDto[] GetEvents(int ticketId, int? eventType = null)
        {
            return _context.OpDataEvents
                .AsNoTracking()
                .Where(x =>
                    x.TicketId == ticketId && (eventType == null || x.OpDataEventType == eventType))
                .AsEnumerable()
                .Select(x => new OpDataEventDto
                {
                    Period = x.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                    User = x.EmployeeId,
                    PlatformNumber = x.NodeId % 10,
                    Cause = x.Cause,
                    Weight = x.Weight,
                    TypeOfTransaction = x.TypeOfTransaction
                })
                .ToArray();
        }

        public SingleWindowOpDataDetail GetSingleWindowOpDataDetail(Guid id)
        {
            var dao = _context.SingleWindowOpDatas.AsNoTracking().First(x => x.Id == id);

            ProductContentList productContents = null;
            if (dao.ProductContents != null)
            {
                var daoProductContentList = JsonConvert.DeserializeObject<List<ProductContent>>(dao.ProductContents);
                
                productContents = new ProductContentList
                {
                    Items = daoProductContentList?.Select(e => new ProductContentItem
                    {
                        No = e.No,
                        OrderNumber = e.OrderNumber,
                        ProductId = e.ProductId,
                        ProductName = _externalDataRepository.GetProductDetail(e.ProductId)?.ShortName ?? "- Хибний ключ -",
                        UnitId = e.UnitId,
                        UnitName = _externalDataRepository.GetMeasureUnitDetail(e.UnitId)?.ShortName ?? "- Хибний ключ -",
                        Quantity = e.Quantity
                    }).ToList()
                };
            }

            var dto = new SingleWindowOpDataDetail
            {
                Id = dao.Id,
                NodeId = dao.NodeId,
                TicketContainerId = dao.TicketContainerId,
                TicketId = dao.TicketId,
                StateId = dao.StateId,
                CheckInDateTime = dao.CheckInDateTime,
                CheckOutDateTime = dao.CheckOutDateTime,
                ContactPhoneNo = dao.ContactPhoneNo,
                LoadTarget = dao.LoadTarget,
                ContractCarrier = dao.ContractCarrierId.HasValue
                    ? _externalDataRepository.GetContractDetail(dao.ContractCarrierId.Value)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                OrganizationId = dao.OrganizationId,
                CreateOperatorId = dao.CreateOperatorId,
                CreateOperatorName = dao.CreateOperatorId.HasValue
                    ? _externalDataRepository.GetExternalEmployeeDetail(dao.CreateOperatorId.Value)?.ShortName ?? "- Хибний ключ -"
                    : string.Empty,
                CreateDate = dao.CreateDate,
                EditOperatorId = dao.EditOperatorId,
                EditOperatorName = dao.EditOperatorId.HasValue
                    ? _externalDataRepository.GetExternalEmployeeDetail(dao.EditOperatorId.Value)?.ShortName ?? "- Хибний ключ -"
                    : string.Empty,
                EditDate = dao.EditDate,
                DocumentTypeId = dao.DocumentTypeId,
                StockId = dao.StockId,
                ReceiverTypeId = dao.ReceiverTypeId,
                ReceiverId = dao.ReceiverId,
                ReceiverAnaliticsId = dao.ReceiverAnaliticsId,
                ProductId = dao.ProductId,
                HarvestId = dao.HarvestId,
                DriverOneId = dao.DriverOneId,
                DriverTwoId = dao.DriverTwoId,
                TransportId = dao.TransportId,
                HiredDriverCode = dao.HiredDriverCode,
                TransportNumber = dao.TransportNumber,
                TrailerNumber = dao.TrailerNumber,
                IncomeInvoiceSeries = dao.IncomeInvoiceSeries,
                IncomeInvoiceNumber = dao.IncomeInvoiceNumber,
                ReceiverDepotId = dao.ReceiverDepotId,
                IsThirdPartyCarrier = dao.IsThirdPartyCarrier,
                CarrierCode = dao.CarrierCode,
                BuyBudgetId = dao.BuyBudgetId,
                SellBudgetId = dao.SellBudgetId,
                PackingWeightValue = dao.PackingWeightValue,
                KeeperOrganizationId = dao.KeeperOrganizationId,
                OrderCode = dao.OrderCode,
                SupplyCode = dao.SupplyCode,
                SupplyTypeId = dao.SupplyTypeId,
                RegistrationTime = dao.RegistrationDateTime,
                DocHumidityValue = dao.DocHumidityValue,
                DocImpurityValue = dao.DocImpurityValue,
                DocNetValue = dao.DocNetValue,
                DocNetDateTime = dao.DocNetDateTime,
                TrailerId = dao.TrailerId,
                TripTicketNumber = dao.TripTicketNumber,
                TripTicketDateTime = dao.TripTicketDateTime,
                WarrantSeries = dao.WarrantSeries,
                WarrantNumber = dao.WarrantNumber,
                WarrantDateTime = dao.WarrantDateTime,
                WarrantManagerName = dao.WarrantManagerName,
                RuleNumber = dao.RuleNumber,
                IncomeDocGrossValue = dao.IncomeDocGrossValue,
                IncomeDocTareValue = dao.IncomeDocTareValue,
                IncomeDocDateTime = dao.IncomeDocDateTime,
                Comments = dao.Comments,
                SupplyTransportTypeId = dao.SupplyTransportTypeId,
                BatchNumber = dao.BatchNumber,
                CarrierRouteId = dao.CarrierRouteId,
                DeliveryBillId = dao.DeliveryBillId,
                DeliveryBillCode = dao.DeliveryBillCode,
                InformationCarrier = dao.InformationCarrier,
                LoadTargetDeviationPlus = dao.LoadTargetDeviationPlus,
                LoadTargetDeviationMinus = dao.LoadTargetDeviationMinus,
                ProductContentList = productContents ?? new ProductContentList(),
                CarrierId = dao.CarrierId,
                CustomPartnerName = dao.CustomPartnerName,
                SupplyTypeName = dao.SupplyTypeId.HasValue
                    ? _externalDataRepository.GetSupplyTypeDetail(dao.SupplyTypeId.Value)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                DocumentTypeName = !string.IsNullOrWhiteSpace(dao.DocumentTypeId)
                    ? _externalDataRepository.GetDeliveryBillTypeDetail(dao.DocumentTypeId)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                OrganizationName = !string.IsNullOrEmpty(dao.OrganizationTitle) ? dao.OrganizationTitle : dao.OrganizationId.HasValue
                    ? _externalDataRepository.GetOrganisationDetail(dao.OrganizationId.Value)?.ShortName ?? "- Хибний ключ -"
                    : string.Empty,
                KeeperOrganizationName = dao.KeeperOrganizationId.HasValue
                    ? _externalDataRepository.GetOrganisationDetail(dao.KeeperOrganizationId.Value)?.ShortName ?? "- Хибний ключ -"
                    : string.Empty,
                StockName = dao.StockId.HasValue
                    ? _externalDataRepository.GetStockDetail(dao.StockId.Value)?.ShortName ?? "- Хибний ключ -"
                    : string.Empty,
                ReceiverTypeName = dao.ReceiverTypeId.HasValue
                    ? _externalDataRepository.GetOriginTypeDetail(dao.ReceiverTypeId.Value)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                ReceiverName = !string.IsNullOrEmpty(dao.ReceiverTitle) ? dao.ReceiverTitle : dao.ReceiverId.HasValue
                    ? _externalDataRepository.GetStockDetail(dao.ReceiverId.Value)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(dao.ReceiverId.Value)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(dao.ReceiverId.Value)?.ShortName
                      ?? "- Хибний ключ -"
                    : string.Empty,
                ReceiverAnaliticsName = dao.ReceiverAnaliticsId.HasValue
                    ? _externalDataRepository.GetCropDetail(dao.ReceiverAnaliticsId.Value)?.Name
                      ?? _externalDataRepository.GetContractDetail(dao.ReceiverAnaliticsId.Value)?.Name
                      ?? "- Хибний ключ -"
                    : string.Empty,
                ProductName = !string.IsNullOrEmpty(dao.ProductTitle) ? dao.ProductTitle : dao.ProductId.HasValue
                    ? _externalDataRepository.GetProductDetail(dao.ProductId.Value)?.ShortName ?? "- Хибний ключ -"
                    : string.Empty,
                HarvestName = dao.HarvestId.HasValue
                    ? _externalDataRepository.GetYearOfHarvestDetail(dao.HarvestId.Value)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                BuyBudgetName = dao.BuyBudgetId.HasValue
                    ? _externalDataRepository.GetBudgetDetail(dao.BuyBudgetId.Value)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                SellBudgetName = dao.SellBudgetId.HasValue
                    ? _externalDataRepository.GetBudgetDetail(dao.SellBudgetId.Value)?.Name ?? "- Хибний ключ -"
                    : string.Empty,
                FirstTareTime = dao.FirstTareTime,
                LastTareTime = dao.LastTareTime,
                TareOperatorId = dao.TareOperatorId,
                TareOperatorName = dao.TareOperatorId.HasValue ? _externalDataRepository.GetExternalEmployeeDetail(dao.TareOperatorId.Value)?.ShortName : string.Empty,
                TareValue = dao.TareValue,
                TrailerTareValue = dao.TrailerTareValue,
                FirstGrossTime = dao.FirstGrossTime,
                LastGrossTime = dao.LastGrossTime,
                GrossOperatorId = dao.GrossOperatorId,
                GrossOperatorName = dao.GrossOperatorId.HasValue ? _externalDataRepository.GetExternalEmployeeDetail(dao.GrossOperatorId.Value)?.ShortName : string.Empty,
                GrossValue = dao.GrossValue,
                TrailerGrossValue = dao.TrailerGrossValue,
                ScaleInNumber = _opDataRepository.GetFirstProcessed<ScaleOpData>(e => e.TicketId == dao.TicketId)?.NodeId ?? 0,
                ScaleOutNumber = _opDataRepository.GetLastProcessed<ScaleOpData>(e => e.TicketId == dao.TicketId)?.NodeId ?? 0,
                NetValue = dao.NetValue,
                WeightDeltaValue = dao.WeightDeltaValue,
                InTime = dao.InTime,
                OutTime = dao.OutTime,
                StampList = dao.StampList,
                LabHumidityTypeId = dao.LabHumidityName,
                LabHumidityTypeName = _externalDataRepository.GetLabHumidityСlassifierDetail(dao.LabHumidityName)?.Name,
                LabImpurityTypeId = dao.LabImpurityName,
                LabImpurityTypeName = _externalDataRepository.GetLabImpurityСlassifierDetail(dao.LabImpurityName)?.Name,
                LabIsInfectioned = dao.LabIsInfectioned,
                LabHumidityValue = dao.LabHumidityValue,
                LabImpurityValue = dao.LabImpurityValue,
                LabOilContentValue = dao.LabOilContentValue,
                LaboratoryOperatorId = dao.LabolatoryOperatorId,
                LaboratoryOperatorName = dao.LabolatoryOperatorId.HasValue ? _externalDataRepository.GetExternalEmployeeDetail(dao.LabolatoryOperatorId.Value)?.ShortName : string.Empty,
                LabFileId = _ticketRepository.GetFirstOrDefault<TicketFile, int>(item => item.TicketId == dao.TicketId)?.Id,
                CollectionPointId = dao.CollectionPointId,
                ReturnCauseId = dao.ReturnCauseId,
                LoadOutDateTime = dao.LoadOutDateTime,
                LoadingOperatorId = dao.LoadingOperatorId,
                LoadingOperatorName = dao.LoadingOperatorId.HasValue ? _externalDataRepository.GetExternalEmployeeDetail(dao.LoadingOperatorId.Value)?.ShortName : string.Empty,
                StatusType = dao.StatusType
            };
            return dto;
        }

        public SingleWindowOpData FetchRouteResults(int ticketId)
        {
            var singleWindowOpData = 
                _context.SingleWindowOpDatas.Where(e =>
                        e.TicketId == ticketId
                        && (e.StateId == OpDataState.Processed || e.StateId == OpDataState.Init))
                    .OrderByDescending(e => e.CheckInDateTime)
                    .FirstOrDefault();
            singleWindowOpData.FetchSingleWindowOpData(singleWindowOpData);
            singleWindowOpData.FetchSecurityCheckInOpData(
                _context.SecurityCheckInOpDatas
                    .Where(x => x.TicketId == ticketId 
                                && x.StateId == OpDataState.Processed)
                    .OrderByDescending(x => x.CheckInDateTime)
                    .FirstOrDefault());
            singleWindowOpData.FetchSecurityCheckOutOpData(
                _context.SecurityCheckOutOpDatas
                    .Where(x => x.TicketId == ticketId 
                                && x.StateId == OpDataState.Processed)
                    .OrderByDescending(x => x.CheckInDateTime)
                    .FirstOrDefault());
            singleWindowOpData.FetchFirstTareScaleOpData(
                _context.ScaleOpDatas
                    .Where(x => x.TicketId == ticketId
                                && x.StateId == OpDataState.Processed
                                && x.TypeId == ScaleOpDataType.Tare)
                    .OrderBy(x => x.CheckInDateTime)
                    .FirstOrDefault());
            singleWindowOpData.FetchLastTareScaleOpData(
                _context.ScaleOpDatas
                    .Where(e => e.TicketId == ticketId 
                                && e.TypeId == ScaleOpDataType.Tare)
                    .OrderByDescending(x => x.CheckInDateTime)
                    .FirstOrDefault());
            singleWindowOpData.FetchFirstGrossScaleOpData(
                _context.ScaleOpDatas
                    .Where(x => x.TicketId == ticketId
                                && x.StateId == OpDataState.Processed
                                && x.TypeId == ScaleOpDataType.Gross)
                    .OrderBy(x => x.CheckInDateTime)
                    .FirstOrDefault());
            singleWindowOpData.FetchLastGrossScaleOpData(
                _context.ScaleOpDatas
                    .Where(x => x.TicketId == ticketId
                                && x.TypeId == ScaleOpDataType.Gross)
                    .OrderByDescending(x => x.CheckInDateTime)
                    .FirstOrDefault());
            singleWindowOpData.FetchWeightResultsOpData();
            singleWindowOpData.FetchLabFacelessOpData(
                _context.LabFacelessOpDatas.FirstOrDefault(item => item.TicketId == ticketId));
            singleWindowOpData.FetchLoadOpData(  
                _context.LoadPointOpDatas
                .Where(x => x.TicketId == ticketId
                            && x.StateId == OpDataState.Processed)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault());
            singleWindowOpData.FetchUnloadOpData(
                _context.UnloadPointOpDatas
                .Where(x => x.TicketId == ticketId
                            && x.StateId == OpDataState.Processed)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault());
            var firstScale = 
                _context.ScaleOpDatas
                .Where(e => e.TicketId == ticketId && e.StateId == OpDataState.Processed)
                .OrderBy(x => x.CheckInDateTime)
                .FirstOrDefault()?.NodeId;
            var secondScale = _context.ScaleOpDatas
                .Where(e => e.TicketId == ticketId && e.StateId == OpDataState.Processed)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault()?.NodeId;
            if (firstScale.HasValue && singleWindowOpData != null)
                singleWindowOpData.ScaleInNumber = int.Parse(firstScale.Value.ToString().Last().ToString());
            if (secondScale.HasValue && singleWindowOpData != null)
                singleWindowOpData.ScaleOutNumber = int.Parse(secondScale.Value.ToString().Last().ToString());

            return singleWindowOpData;
        }

        public LabFacelessOpData GetLabFacelessOpDataDto(Guid id)
        {
            var dao = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == id);

            if (dao == null) return null;

            var dto = new LabFacelessOpData
            {
                Id = dao.Id,
                NodeId = dao.NodeId,
                TicketId = dao.TicketId,
                StateId = dao.StateId,
                CheckInDateTime = dao.CheckInDateTime,
                CheckOutDateTime = dao.CheckOutDateTime,
                ImpurityClassId = dao.ImpurityClassId,
                ImpurityValue = dao.ImpurityValue,
                HumidityClassId = dao.HumidityClassId,
                HumidityValue = dao.HumidityValue,
                InfectionedClassId = dao.InfectionedClassId,
                EffectiveValue = dao.EffectiveValue,
                Comment = dao.Comment,
                LabFacelessOpDataItemSet = dao.LabFacelessOpDataComponentSet.Select(e =>
                    new LabFacelessOpDataComponent
                    {
                        Id = e.Id,
                        CheckInDateTime = e.CheckInDateTime,
                        DataSourceName = e.DataSourceName,
                        LabFacelessOpDataId = e.LabFacelessOpDataId,
                        NodeId = e.NodeId,
                        StateId = e.StateId,
                        AnalysisTrayRfid = e.AnalysisTrayRfid,
                        AnalysisValueDescriptor = JsonConvert.DeserializeObject<AnalysisValueDescriptor>(e.AnalysisValueDescriptor),
                        ImpurityClassId = e.ImpurityClassId,
                        ImpurityValue = e.ImpurityValue,
                        HumidityClassId = e.HumidityClassId,
                        HumidityValue = e.HumidityValue,
                        InfectionedClassId = e.InfectionedClassId,
                        EffectiveValue = e.EffectiveValue,
                        Comment = e.Comment
                    }).ToList()
            };

            return dto;
        }

        public BasicTicketContainerData GetBasicTicketData(int ticketId)
        {
            var data = new BasicTicketContainerData();
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId);
            if (singleWindowOpData != null)
            {
                data.IsTechRoute = singleWindowOpData.SupplyCode == TechRoute.SupplyCode;
                data.ReceiverDepotName = singleWindowOpData.ReceiverDepotId != null
                    ? _context.Stocks.FirstOrDefault(e => e.Id == singleWindowOpData.ReceiverDepotId)?.ShortName ?? string.Empty
                    : string.Empty;
                data.LoadTarget = singleWindowOpData.LoadTarget;
                data.ProductName = singleWindowOpData.ProductId.HasValue 
                    ? _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId.Value)?.ShortName ??
                                   singleWindowOpData.ProductTitle ??
                                   string.Empty : string.Empty;

                data.SenderName = singleWindowOpData.ReceiverId.HasValue
                    ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId.Value)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId.Value)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId.Value)?.ShortName
                      ?? singleWindowOpData.CustomPartnerName
                      ?? "- Хибний ключ -"
                    : string.Empty;

                data.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier;
                data.SingleWindowComment = singleWindowOpData.Comments;
                data.DeliveryBillCode = singleWindowOpData.DeliveryBillCode;
                data.StampList = singleWindowOpData.StampList;
                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    data.TransportNo = singleWindowOpData.TransportNumber;
                    data.TrailerNo = singleWindowOpData.TrailerNumber;
                }
                else
                {
                    data.TransportNo =
                        singleWindowOpData.TransportId.HasValue 
                            ? _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo
                            : string.Empty;
                    data.TrailerNo =
                        singleWindowOpData.TrailerId.HasValue
                            ? _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo
                            : string.Empty;
                }
            }

            return data;
        }
    }
}