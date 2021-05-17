// ReSharper disable once CheckNamespace

using System;
using System.Collections;

namespace Gravitas.Model.Dto
{
    public class SingleWindowOpDataDetail : BaseOpDataDetail
    {
        public string ContactPhoneNo { get; set; }
        public double LoadTarget { get; set; }

        // 1
        public string OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        // 3
        public string CreateOperatorId { get; set; }
        public string CreateOperatorName { get; set; }

        // 4
        public DateTime? CreateDate { get; set; }

        // 5
        public string EditOperatorId { get; set; }

        public string EditOperatorName { get; set; }

        // 6
        public DateTime? EditDate { get; set; }

        // 8
        public string DocumentTypeId { get; set; }

        public string DocumentTypeName { get; set; }

        // 9
        public string StockId { get; set; }

        public string StockName { get; set; }

        // 10
        public string ReceiverTypeId { get; set; }

        public string ReceiverTypeName { get; set; }

        // 11
        public string ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        // 12
        public string ReceiverAnaliticsId { get; set; }

        public string ReceiverAnaliticsName { get; set; }

        // 13
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        // 14
        public string HarvestId { get; set; }

        public string HarvestName { get; set; }

        // 15
        public double? GrossValue { get; set; }

        // 16
        public double? TareValue { get; set; }

        // 17
        public double? NetValue { get; set; }

        // 19
        public string DriverOneId { get; set; }

        // 20
        public string DriverTwoId { get; set; }

        // 21
        public string TransportId { get; set; }

        // 22
        public string HiredDriverCode { get; set; }

        // 23
        public string HiredTansportNumber { get; set; }

        // 25
        public string IncomeInvoiceSeries { get; set; }

        // 26
        public string IncomeInvoiceNumber { get; set; }

        // 27
        public string ReceiverDepotId { get; set; }

        // 28
        public bool IsThirdPartyCarrier { get; set; }

        // 29
        public string CarrierCode { get; set; }

        // 31
        public string BuyBudgetId { get; set; }

        public string BuyBudgetName { get; set; }

        // 32
        public string SellBudgetId { get; set; }

        public string SellBudgetName { get; set; }

        // 37
        public double? PackingWeightValue { get; set; }

        // 38
        public string KeeperOrganizationId { get; set; }

        public string KeeperOrganizationName { get; set; }

        // 39
        public string OrderCode { get; set; }

        // 40
        public string SupplyCode { get; set; }

        // 41
        public string SupplyTypeId { get; set; }

        public string SupplyTypeName { get; set; }

        // 42
        public DateTime? RegistrationTime { get; set; }

        // 43
        public DateTime? InTime { get; set; }

        // 44
        public DateTime? OutTime { get; set; }

        // 45
        public DateTime? FirstGrossTime { get; set; }

        // 46
        public DateTime? FirstTareTime { get; set; }

        // 47
        public DateTime? LastGrossTime { get; set; }

        // 48
        public DateTime? LastTareTime { get; set; }

        // 49
        public string CollectionPointId { get; set; }

        // 51
        public string LabHumidityTypeId { get; set; }

        public string LabHumidityTypeName { get; set; }

        // 52
        public string LabImpurityTypeId { get; set; }

        public string LabImpurityTypeName { get; set; }

        // 53
        public bool LabIsInfectioned { get; set; }

        // 54
        public double? LabHumidityValue { get; set; }

        // 55
        public double? LabImpurityValue { get; set; }

        // 56
        public double? DocHumidityValue { get; set; }

        // 57
        public double? DocImpurityValue { get; set; }

        // 58
        public double? DocNetValue { get; set; }

        // 59
        public DateTime? DocNetDateTime { get; set; }

        // 61
        public string ReturnCauseId { get; set; }

        // 62
        public string TrailerId { get; set; }

        // 63
        public string TrailerNumber { get; set; }

        // 64
        public string TripTicketNumber { get; set; }

        // 65
        public DateTime? TripTicketDateTime { get; set; }

        // 66
        public string WarrantSeries { get; set; }

        // 67
        public string WarrantNumber { get; set; }

        // 68
        public DateTime? WarrantDateTime { get; set; }

        // 69
        public string WarrantManagerName { get; set; }

        // 70
        public string StampList { get; set; }

        // 71
        public string StatusType { get; set; }

        // 73
        public string RuleNumber { get; set; }

        // 74
        public double? TrailerGrossValue { get; set; }

        // 75
        public double? TrailerTareValue { get; set; }

        // 76
        public double? IncomeDocGrossValue { get; set; }

        // 77
        public double? IncomeDocTareValue { get; set; }

        // 78
        public DateTime? IncomeDocDateTime { get; set; }

        // 79
        public string Comments { get; set; }

        // 80
        public double? WeightDeltaValue { get; set; }

        // 81
        public string SupplyTransportTypeId { get; set; }

        // 82
        public string LabolatoryOperatorId { get; set; }

        public string LabolatoryOperatorName { get; set; }

        // 83
        public string GrossOperatorId { get; set; }

        public string GrossOperatorName { get; set; }

        // 86
        public long ScaleInNumber { get; set; }

        // 87
        public long ScaleOutNumber { get; set; }

        // 92
        public string BatchNumber { get; set; }

        // 93
        public string TareOperatorId { get; set; }

        public string TareOperatorName { get; set; }

        // 101
        public string LoadingOperatorId { get; set; }

        public string LoadingOperatorName { get; set; }

        // 102
        public DateTime? LoadOutDateTime { get; set; }

        // 103
        public string CarrierRouteId { get; set; }

        // 104
        public double? LabOilContentValue { get; set; }

        // 106
        public string DeliveryBillId { get; set; }

        // 105
        public string DeliveryBillCode { get; set; }

        // 107
        public string InformationCarrier { get; set; }

        // 108
        public int LoadTargetDeviationPlus { get; set; }

        // 109
        public int PackingWeightDeviationPlus { get; set; }

        // 110
        public int LoadTargetDeviationMinus { get; set; }

        // 111
        public int PackingWeightDeviationMinus { get; set; }

        // 112
        public long? LabFileId { get; set; }

        // 113
        public string CustomPartnerName { get; set; }

        public string CarrierId { get; set; }
        public string BarCode { get; set; }
        public string ContractCarrier { get; set; }
        public bool IsPreRegistered { get; set; }

        public ProductContentList ProductContentList { get; set; }
    }
}