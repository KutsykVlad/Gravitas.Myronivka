using System;
using Gravitas.Model.DomainModel.OpData.TDO.Json;

namespace Gravitas.Model.DomainModel.OpData.TDO.Detail
{
    public class SingleWindowOpDataDetail : BaseOpDataDetail
    {
        public string ContactPhoneNo { get; set; }
        public double LoadTarget { get; set; }
        public Guid? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public Guid? CreateOperatorId { get; set; }
        public string CreateOperatorName { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? EditOperatorId { get; set; }
        public string EditOperatorName { get; set; }
        public DateTime? EditDate { get; set; }
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public Guid? StockId { get; set; }
        public string StockName { get; set; }
        public Guid? ReceiverTypeId { get; set; }
        public string ReceiverTypeName { get; set; }
        public Guid? ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public Guid? ReceiverAnaliticsId { get; set; }
        public string ReceiverAnaliticsName { get; set; }
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid? HarvestId { get; set; }
        public string HarvestName { get; set; }
        public double? GrossValue { get; set; }
        public double? TareValue { get; set; }
        public double? NetValue { get; set; }
        public Guid? DriverOneId { get; set; }
        public Guid? DriverTwoId { get; set; }
        public Guid? TransportId { get; set; }
        public string HiredDriverCode { get; set; }
        public string HiredTransportNumber { get; set; }
        public string IncomeInvoiceSeries { get; set; }
        public string IncomeInvoiceNumber { get; set; }
        public string CollectionPointName { get; set; }
        public string ReturnCauseName { get; set; }
        public string CarrierRouteName { get; set; }
        public Guid? ReceiverDepotId { get; set; }
        public bool IsThirdPartyCarrier { get; set; }
        public string CarrierCode { get; set; }
        public Guid? BuyBudgetId { get; set; }
        public string BuyBudgetName { get; set; }
        public Guid? SellBudgetId { get; set; }
        public string SellBudgetName { get; set; }
        public double? PackingWeightValue { get; set; }
        public Guid? KeeperOrganizationId { get; set; }
        public string KeeperOrganizationName { get; set; }
        public string OrderCode { get; set; }
        public string SupplyCode { get; set; }
        public Guid? SupplyTypeId { get; set; }
        public string SupplyTypeName { get; set; }
        public DateTime? RegistrationTime { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public DateTime? FirstGrossTime { get; set; }
        public DateTime? FirstTareTime { get; set; }
        public DateTime? LastGrossTime { get; set; }
        public DateTime? LastTareTime { get; set; }
        public Guid? CollectionPointId { get; set; }
        public string LabHumidityTypeId { get; set; }
        public string LabHumidityTypeName { get; set; }
        public string LabImpurityTypeId { get; set; }
        public string LabImpurityTypeName { get; set; }
        public bool LabIsInfectioned { get; set; }
        public double? LabHumidityValue { get; set; }
        public double? LabImpurityValue { get; set; }
        public double? DocHumidityValue { get; set; }
        public double? DocImpurityValue { get; set; }
        public double? DocNetValue { get; set; }
        public DateTime? DocNetDateTime { get; set; }
        public string ReturnCauseId { get; set; }
        public Guid? TrailerId { get; set; }
        public string TrailerNumber { get; set; }
        public string TripTicketNumber { get; set; }
        public DateTime? TripTicketDateTime { get; set; }
        public string WarrantSeries { get; set; }
        public string WarrantNumber { get; set; }
        public DateTime? WarrantDateTime { get; set; }
        public string WarrantManagerName { get; set; }
        public string StampList { get; set; }
        public string StatusType { get; set; }
        public string RuleNumber { get; set; }
        public double? TrailerGrossValue { get; set; }
        public double? TrailerTareValue { get; set; }
        public double? IncomeDocGrossValue { get; set; }
        public double? IncomeDocTareValue { get; set; }
        public DateTime? IncomeDocDateTime { get; set; }
        public string Comments { get; set; }
        public double? WeightDeltaValue { get; set; }
        public string SupplyTransportTypeId { get; set; }
        public Guid? LaboratoryOperatorId { get; set; }
        public string LaboratoryOperatorName { get; set; }
        public Guid? GrossOperatorId { get; set; }
        public string GrossOperatorName { get; set; }
        public int ScaleInNumber { get; set; }
        public int ScaleOutNumber { get; set; }
        public string BatchNumber { get; set; }
        public Guid? TareOperatorId { get; set; }
        public string TareOperatorName { get; set; }
        public Guid? LoadingOperatorId { get; set; }
        public string LoadingOperatorName { get; set; }
        public DateTime? LoadOutDateTime { get; set; }
        public string CarrierRouteId { get; set; }
        public double? LabOilContentValue { get; set; }
        public string DeliveryBillId { get; set; }
        public string DeliveryBillCode { get; set; }
        public string InformationCarrier { get; set; }
        public int LoadTargetDeviationPlus { get; set; }
        public int PackingWeightDeviationPlus { get; set; }
        public int LoadTargetDeviationMinus { get; set; }
        public int PackingWeightDeviationMinus { get; set; }
        public int? LabFileId { get; set; }
        public string CustomPartnerName { get; set; }
        public Guid? CarrierId { get; set; }
        public string BarCode { get; set; }
        public string ContractCarrier { get; set; }

        public ProductContentList ProductContentList { get; set; }
    }
}