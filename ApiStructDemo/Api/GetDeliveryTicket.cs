using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStructDemo.Api
{
	class GetDeliveryBillViaSupplyCode_Request
	{
		// 41
		public string SupplyTypeCode = "Some string value";
	}

	class GetDeliveryBillViaId_Request
	{
		// 105
		public string Id = "Some string value";
	}

	class GetDeliveryBillViaSupplyCode_Response
	{
		// ErrorCode
		public int ErrorCode = 0;
		// ErrorName
		public string ErrorMsg = "Some string value";

		// 105
		public string Id = "Some string value";
		// 41
		public string SupplyTypeCode = "Some string value";
		// 39
		public string OrderCode = "Some string value";
		// 8
		public string DocumentType = "Some string value";
		// 1
		public string OrganisationCode = "Some string value";
		// 38
		public string SenderOrganisationCode = "Some string value";
		// 9
		public string SenderDepotCode = "Some string value";
		// 10
		public string ReceiverTypeCode = "Some string value";
		// 11
		public string ReceiverCode = "Some string value";
		// 12
		public string ReceiverAnaliticsCode = "Some string value";
		// 13
		public string NomenclatureCode = "Some string value";
		// 14
		public string HarvestCode = "Some string value";
		// 31
		public string BuyСlauseCode = "Some string value";
		// 32
		public string SellClauseCode = "Some string value";

		// Manager Name
		public string ManagerName = "Some string value";
		// Manager E-mail
		public string ManagerEmail = "Some string value";
		// Manager Name
		public string ManagerPhone = "Some string value";
	}

	class PostDeliveryTicket_Request
	{
		// 105
		public string Id = "Some string value";
		// 3
		public string CreateOperatorNameAD = "Some string value";
		// 4
		public DateTime CreteDate = DateTime.Now;
		// 6
		public DateTime EditDate = DateTime.Now;
		// 42
		public DateTime RegistrationDateTime = DateTime.Now;
		// 43
		public DateTime InDateTime = DateTime.Now;
		// 44
		public DateTime OutDateTime = DateTime.Now;
		// 45
		public DateTime FirstTareDateTime = DateTime.Now;
		// 46
		public DateTime LastTareDateTime = DateTime.Now;
		// 47
		public DateTime FirstGrossDateTime = DateTime.Now;
		// 48
		public DateTime LastGrossDateTime = DateTime.Now;
		// 5
		public string EditOperatorNameAD = "Some string value";

		// 15
		public double GrossValue = 123456.00;
		// 16
		public double TareValue = 123456.00;
		// 17
		public double NetValue = 123456.00;
		// 19
		public string DriverOneCode = "Some string value";
		// 20
		public string DriverTwoCode = "Some string value";
		// 21
		public string TransportCode = "Some string value";
		// 22
		public string HiredDriverCode = "Some string value";
		// 23
		public string HiredTansportNo = "Some string value";
		// 25
		public string IncomeInvoiceSeries = "Some string value";
		// 26
		public string IncomeInvoiceNo = "Some string value";
		// 27
		public string ReceiverDepotCode = "Some string value";
		// 28
		public bool IsThirdPartyCarrier = true;
		// 29
		public string CarrierCode = "Some string value";
		// 31
		public string BuyСlauseCode = "Some string value";
		// 32
		public string SellClauseCode = "Some string value";
		// 37
		public double PackingWeightValue = 123456.00;
		// 40
		public string SupplyCode = "Some string value";
		
		// 49
		public string CollectionPointCode = "Some string value";
		// 51
		public string LabHumidityName = "Some string value";
		// 52
		public string LabImpurityName = "Some string value";
		// 53
		public bool LabIsInfectioned = true;
		// 54
		public double LabHumidityValue = 123456.00;
		// 55
		public double LabImpurityValue = 123456.00;
		// 56
		public double DocHumidityValue = 123456.00;
		// 57
		public double DocImpurityValue = 123456.00;
		// 58
		public double DocNetValue = 123456.00;
		// 59
		public DateTime DocNetDateTime = DateTime.Now;
		// 61
		public string ReturnCauseCode = "Some string value";
		// 62
		public string TrailerCode = "Some string value";
		// 63
		public string TrailerNo = "Some string value";
		// 64
		public string TripTicketNo = "Some string value";
		// 65
		public DateTime TripTicketDateTime = DateTime.Now;
		// 66
		public string WarrantSeries = "Some string value";
		// 67
		public string WarrantNo = "Some string value";
		// 68
		public string WarrantDatetime = "Some string value";
		// 69
		public string WarrantManagerName = "Some string value";
		// 70
		public string StampList = "Some string value";
		// 73
		public string RuleNo = "Some string value";
		// 74
		public double TrailerGrossValue = 123456.00;
		// 75
		public double TrailerTareValue = 123456.00;
		// 76
		public double IncomeDocGrossValue = 123456.00;
		// 77
		public double IncomeDocTareValue = 123456.00;
		// 78
		public DateTime IncomeDocDateTime = DateTime.Now;
		// 79
		public string Comments = "Some string value";
		// 80
		public double WeightDeltaValue = 123456.00;
		// 81
		public string SupplyTypeCode = "Some string value";
		// 82
		public string LabolatoryOperatorNameAD = "Some string value";
		// 83
		public string GrossOperatorNameAD = "Some string value";
		// 86
		public int ScaleInNo = 1;
		// 87
		public int ScaleOutNo = 1;
		// 92
		public string BatchNo = "Some string value";
		// 93
		public string TareOperatorNameAD = "Some string value";
		// 101
		public string LoadingOperatorNameAD = "Some string value";
		// 102
		public DateTime LoadOutDateTime = DateTime.Now;
		// 103
		public string CarrierRouteCode = "Some string value";
		// 104
		public double LabOilContentValue = 123456.00;
	}

	class PostDeliveryTicket_Response {
		// ErrorCode
		public int ErrorCode = 0;
		// ErrorName
		public string ErrorMsg = "Some string value";
		// 105
		public string Id = "Some string value";
		// 71
		public string BillStatus = "Some string value";
	}

	class GetDictionaryValues_Request {
		public string DictionaryType = "Some string value";
		public string RecordId = "Some string value";
	}

	class MarkDictionaryEntry_Request
	{
		public string DictionaryType = "Some string value";
		public string RecordId = "Some string value";
		public string UpdatedStatus = "Some string value";
	}

	class GetBillManager_Response
	{
		// ErrorCode
		public int ErrorCode = 0;
		// ErrorName
		public string ErrorMsg = "Some string value";

		// 105
		public string Id = "Some string value";

		// Manager Name
		public string ManagerName = "Some string value";
		// Manager E-mail
		public string ManagerEmail = "Some string value";
		// Manager Name
		public string ManagerPhone = "Some string value";
	}

	class GetDictionaryValues_Type1_Response {
		// Key
		public string Id = "Some string value";
		// ShortName
		public string ShortName = "Some string value";
		// FullName
		public string FullName = "Some string value";
		// FullName
		public string Address = "Some string value";
	}

	class GetDictionaryValues_Type2_Response
	{
		// Key
		public string Id = "Some string value";
		// ShortName
		public string ShortName = "Some string value";
		// FullName
		public string FullName = "Some string value";
		// FullName
		public string Position = "Some string value";
		// FullName
		public string RoleCode = "Some string value";
		// FullName
		public string ActiveDirectoryName = "Some string value";
	}

	class GetDictionaryValues_Type3_Response
	{
		// Name
		public string ShortName = "Some string value";
	}

	class GetDictionaryValues_Type4_Response
	{
		// Key
		public string Id = "Some string value";
		// Name
		public string Name = "Some string value";
	}

	class GetDictionaryValues_Type5_Response
	{
		// Key
		public string Id = "Some string value";
		// ShortName
		public string ShortName = "Some string value";
		// FullName
		public string FullName = "Some string value";
	}

	class GetDictionaryValues_Type6_Response
	{
		// Key
		public string Id = "Some string value";
		// Brand
		public string Brand = "Some string value";
		// Model
		public string Model = "Some string value";
		// Type Code
		public string TypeCode = "Some string value";
		// Registration No 
		public string RegistrationNo = "Some string value";
	}

	class GetDictionaryValues_Type7_Response
	{
		// Key
		public string Id = "Some string value";
		// Name
		public string Name = "Some string value";
		// StartDateTime
		public DateTime StartDateTime = DateTime.Now;
		// StopDateTime
		public DateTime StopDateTime = DateTime.Now;
	}
}
