using System;


namespace ApiStructDemo.Api
{
	class DeliveryBillSyncData{

		// 1 UI:R Core:R
		public string OrganizationId { get; set; }
		// 2
		// - - -
		// 3 UI:R Core:W
		public string CreateOperatorNameAd { get; set; }
		// 4
		//public DateTime CreteDate { get; set; }
		// 5
		//public string EditOperatorNameAd { get; set; }
		// 6
		//public DateTime EditDate { get; set; }
		// 7
		// - - -
		// 8
		public string DocumentTypeId { get; set; }
		// 9
		public string SenderDepotId { get; set; }
		// 10
		public string ReceiverTypeId { get; set; }
		// 11
		public string ReceiverId { get; set; }
		// 12
		public string ReceiverAnaliticsId { get; set; }
		// 13
		//public string ProductId { get; set; }
		// 14
		public string HarvestId { get; set; }
		// 15
		//public double GrossValue { get; set; }
		// 16
		//public double TareValue { get; set; }
		// 17
		//public double NetValue { get; set; }
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
		// 30
		// - - -
		// 31
		public string BuyBudgetsId { get; set; }
		// 32
		public string SellBudgetsId { get; set; }
		// 33
		// - - -
		// 34
		// - - -
		// 35
		// - - -
		// 36
		// - - -
		// 37
		public double PackingWeightValue { get; set; }
		// 38
		//public string SenderOrganisationId { get; set; }
		// 39
		//public string OrderCode { get; set; }
		// 40
		public string SupplyCode { get; set; }
		// 41
		//public string SupplyTypeId { get; set; }
		// 42
		public DateTime RegistrationTime { get; set; }
		// 43
		public DateTime InTime { get; set; }
		// 44
		public DateTime OutTime { get; set; }
		// 45
		public DateTime FirstGrossTime { get; set; }
		// 46
		public DateTime FirstTareTime { get; set; }
		// 47
		public DateTime LastGrossTime { get; set; }
		// 48
		public DateTime LastTareTime { get; set; }
		// 49
		public string CollectionPointId { get; set; }
		// 50
		// - - -
		// 51
		public string LabHumidityName { get; set; }
		// 52
		public string LabImpurityName { get; set; }
		// 53
		public bool LabIsInfectioned { get; set; }
		// 54
		public double LabHumidityValue { get; set; }
		// 55
		public double LabImpurityValue { get; set; }
		// 56
		public double DocHumidityValue { get; set; }
		// 57
		public double DocImpurityValue { get; set; }
		// 58
		public double DocNetValue { get; set; }
		// 59
		public DateTime DocNetDateTime { get; set; }
		// 60
		// - - -
		// 61
		public string ReturnCauseId { get; set; }
		// 62
		public string TrailerId { get; set; }
		// 63
		public string TrailerNumber { get; set; }
		// 64
		public string TripTicketNumber { get; set; }
		// 65
		public DateTime TripTicketDateTime { get; set; }
		// 66
		public string WarrantSeries { get; set; }
		// 67
		public string WarrantNumber { get; set; }
		// 68
		public string WarrantDatetime { get; set; }
		// 69
		public string WarrantManagerName { get; set; }
		// 70
		public string StampList { get; set; }
		// 71
		public string StatusTypeCode { get; set; }
		// 72
		// - - -
		// 73
		public string RuleNumber { get; set; }
		// 74
		public double TrailerGrossValue { get; set; }
		// 75
		public double TrailerTareValue { get; set; }
		// 76
		public double IncomeDocGrossValue { get; set; }
		// 77
		public double IncomeDocTareValue { get; set; }
		// 78
		public DateTime IncomeDocDateTime { get; set; }
		// 79
		public string Comments { get; set; }
		// 80
		public double WeightDeltaValue { get; set; }
		// 81
		public string SupplyTypeCode { get; set; }
		// 82
		public string LabolatoryOperatorNameAd { get; set; }
		// 83
		public string GrossOperatorNameAd { get; set; }
		// 84
		// - - -
		// 85
		// - - -
		// 86
		public long ScaleInNumber { get; set; }
		// 87
		public long ScaleOutNumber { get; set; }
		// 88
		// - - -
		// 89
		// - - -
		// 90
		// - - -
		// 91
		// - - -
		// 92
		public string BatchNumber { get; set; }
		// 93
		public string TareOperatorNameAd { get; set; }
		// 94
		// - - -
		// 95
		// - - -
		// 96
		// - - -
		// 97
		// - - -
		// 98
		// - - -
		// 99
		// - - -
		// 100
		// - - -
		// 101
		public string LoadingOperatorNameAd { get; set; }
		// 102
		public DateTime LoadOutDateTime { get; set; }
		// 103
		public string CarrierRouteId { get; set; }
		// 104
		public double LabOilContentValue { get; set; }
		// 105
		//public string DeliveryBillId { get; set; }
		// 106
		//public string DeliveryBillCode { get; set; }
		// 107
		public string InformationCarrier { get; set; }
	}
}
