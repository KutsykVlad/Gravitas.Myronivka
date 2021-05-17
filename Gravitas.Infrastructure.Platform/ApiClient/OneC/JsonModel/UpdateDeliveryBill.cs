using System;
using Gravitas.Model.DomainModel.OpDataEvent.DTO;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class UpdateDeliveryBillDto
        {
            public class Request
            {
                [JsonProperty("Activity")]
                public int Activity { get; set; }

                // 106
                [JsonProperty("ID")]
                public string Id { get; set; }

                // 3
                [JsonProperty("CreateOperatorID")]
                public string CreateOperatorId { get; set; }

                // 4
                [JsonProperty("CreateDate")]
                public DateTime? CreateDate { get; set; }

                // 6
                [JsonProperty("EditDate")]
                public DateTime? EditDate { get; set; }

                // 42
                [JsonProperty("RegistrationTime")]
                public DateTime? RegistrationTime { get; set; }

                // 43
                [JsonProperty("InTime")]
                public DateTime? InTime { get; set; }

                // 44
                [JsonProperty("OutTime")]
                public DateTime? OutTime { get; set; }

                // 45
                [JsonProperty("FirstGrossTime")]
                public DateTime? FirstGrossTime { get; set; }

                // 46
                [JsonProperty("FirstTareTime")]
                public DateTime? FirstTareTime { get; set; }

                // 47
                [JsonProperty("LastGrossTime")]
                public DateTime? LastGrossTime { get; set; }

                // 48
                [JsonProperty("LastTareTime")]
                public DateTime? LastTareTime { get; set; }

                // 5
                [JsonProperty("EditOperatorID")]
                public string EditOperatorId { get; set; }

                // 15
                [JsonProperty("GrossValue")]
                public double GrossValue { get; set; }

                // 16
                [JsonProperty("TareValue")]
                public double TareValue { get; set; }

                // 17
                [JsonProperty("NetValue")]
                public double NetValue { get; set; }

                // 19
                [JsonProperty("DriverOneID")]
                public string DriverOneId { get; set; }

                // 20
                [JsonProperty("DriverTwoID")]
                public string DriverTwoId { get; set; }

                // 21
                [JsonProperty("TransportID")]
                public string TransportId { get; set; }

                // 22
                [JsonProperty("HiredDriverCode")]
                public string HiredDriverCode { get; set; }

                // 23
                [JsonProperty("HiredTansportNumber")]
                public string HiredTansportNumber { get; set; }

                // 25
                [JsonProperty("IncomeInvoiceSeries")]
                public string IncomeInvoiceSeries { get; set; }

                // 26
                [JsonProperty("IncomeInvoiceNumber")]
                public string IncomeInvoiceNumber { get; set; }

                // 27
                [JsonProperty("ReceiverStockID")]
                public string ReceiverStockId { get; set; }

                // 28
                [JsonProperty("IsThirdPartyCarrier")]
                public string IsThirdPartyCarrier { get; set; }

                // 29
                [JsonProperty("CarrierCode")]
                public string CarrierCode { get; set; }

                // 31
                [JsonProperty("BuyBudgetsID")]
                public string BuyBudgetsId { get; set; }

                // 32
                [JsonProperty("SellBudgetsID")]
                public string SellBudgetsId { get; set; }

                // 37
                [JsonProperty("PackingWeightValue")]
                public double PackingWeightValue { get; set; }

                // 40
                [JsonProperty("SupplyCode")]
                public string SupplyCode { get; set; }

                // 49
                [JsonProperty("CollectionPointID")]
                public string CollectionPointId { get; set; }

                // 51
                [JsonProperty("LabHumidityType")]
                public string LabHumidityType { get; set; }

                // 52
                [JsonProperty("LabImpurityType")]
                public string LabImpurityType { get; set; }

                // 53
                [JsonProperty("LabIsInfectioned")]
                public string LabIsInfectioned { get; set; }

                // 54
                [JsonProperty("LabHumidityValue")]
                public double LabHumidityValue { get; set; }

                // 55
                [JsonProperty("LabImpurityValue")]
                public double LabImpurityValue { get; set; }

                // 56
                [JsonProperty("DocHumidityValue")]
                public double DocHumidityValue { get; set; }

                // 57
                [JsonProperty("DocImpurityValue")]
                public double DocImpurityValue { get; set; }

                // 58
                [JsonProperty("DocNetValue")]
                public double DocNetValue { get; set; }

                // 59
                [JsonProperty("DocNetDateTime")]
                public DateTime? DocNetDateTime { get; set; }

                // 61
                [JsonProperty("ReturnCauseID")]
                public string ReturnCauseId { get; set; }

                // 62
                [JsonProperty("TrailerID")]
                public string TrailerId { get; set; }

                // 63
                [JsonProperty("TrailerNumber")]
                public string TrailerNumber { get; set; }

                // 64
                [JsonProperty("TripTicketNumber")]
                public string TripTicketNumber { get; set; }

                // 65
                [JsonProperty("TripTicketDateTime")]
                public DateTime? TripTicketDateTime { get; set; }

                // 66
                [JsonProperty("WarrantSeries")]
                public string WarrantSeries { get; set; }

                // 67
                [JsonProperty("WarrantNumber")]
                public string WarrantNumber { get; set; }

                // 68
                [JsonProperty("WarrantDateTime")]
                public DateTime? WarrantDateTime { get; set; }

                // 69
                [JsonProperty("WarrantManagerName")]
                public string WarrantManagerName { get; set; }

                // 70
                [JsonProperty("StampList")]
                public string StampList { get; set; }

                // 73
                [JsonProperty("RuleNumber")]
                public string RuleNumber { get; set; }

                // 74
                [JsonProperty("TrailerGrossValue")]
                public double TrailerGrossValue { get; set; }

                // 75
                [JsonProperty("TrailerTareValue")]
                public double TrailerTareValue { get; set; }

                // 76
                [JsonProperty("IncomeDocGrossValue")]
                public double IncomeDocGrossValue { get; set; }

                // 77
                [JsonProperty("IncomeDocTareValue")]
                public double IncomeDocTareValue { get; set; }

                // 78
                [JsonProperty("IncomeDocDateTime")]
                public DateTime? IncomeDocDateTime { get; set; }

                // 79
                [JsonProperty("Comments")]
                public string Comments { get; set; }

                // 80
                [JsonProperty("WeightDeltaValue")]
                public double WeightDeltaValue { get; set; }

                // 81
                [JsonProperty("SupplyType")]
                public string SupplyType { get; set; }

                // 82
                [JsonProperty("LabolatoryOperatorID")]
                public string LabolatoryOperatorId { get; set; }

                // 83
                [JsonProperty("GrossOperatorID")]
                public string GrossOperatorId { get; set; }

                // 86
                [JsonProperty("ScaleInNumber")]
                public long ScaleInNumber { get; set; }

                // 87
                [JsonProperty("ScaleOutNumber")]
                public long ScaleOutNumber { get; set; }

                // 92
                [JsonProperty("BatchNumber")]
                public string BatchNumber { get; set; }

                // 93
                [JsonProperty("TareOperatorID")]
                public string TareOperatorId { get; set; }

                // 101
                [JsonProperty("LoadingOperatorID")]
                public string LoadingOperatorId { get; set; }

                // 102
                [JsonProperty("LoadOutDate")]
                public DateTime? LoadOutDate { get; set; }

                // 103
                [JsonProperty("CarrierRouteID")]
                public string CarrierRouteId { get; set; }

                // 104
                [JsonProperty("LabOilContentValue")]
                public double LabOilContentValue { get; set; }

                // 107
                [JsonProperty("InformationCarrier")]
                public string InformationCarrier { get; set; }

                [JsonProperty("WeightingStatistics")]
                public OpDataEventDto[] WeightingStatistics { get; set; }
            }

            public class Response : BaseResponseDto
            {
                // 106
                [JsonProperty("ID")]
                public string Id { get; set; }

                // 71
                [JsonProperty("BillStatus")]
                public string BillStatus { get; set; }
            }
        }
    }
}