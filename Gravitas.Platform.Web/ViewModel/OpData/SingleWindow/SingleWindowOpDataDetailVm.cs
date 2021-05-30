using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gravitas.Platform.Web.ViewModel.Employee;
using Gravitas.Platform.Web.ViewModel.OpData.Base;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SingleWindowVms {
		
		public class SingleWindowOpDataDetailVm : BaseOpDataDetailVm
		{
		    [Required(ErrorMessage = "Вам необхідно ввести номер телефону")]
		    [DisplayName("Контактний телефон")]
		    [RegularExpression(@"^(\d{12})$", ErrorMessage = "Не правильно введений номер телефону, введіть 12 цифр")]
            public string ContactPhoneNo { get; set; }
		   
			[DisplayName("Норматив погрузки, кг")]
//			[RegularExpression(@"^(\d{5})$", ErrorMessage = "Не правильно введена вага. Максимум 5 цифр.")]
			public double LoadTarget { get; set; }
			// 1
			[DisplayName("Організація")]
			public string OrganizationId { get; set; }
			public string OrganizationName { get; set; }
			// 3
			[DisplayName("Автор")]
			public string CreateOperatorName { get; set; }
			// 4
			[DisplayName("Дата створення")]
			public DateTime? CreateDate { get; set; }
			// 5
			[DisplayName("Редактор")]
			public string EditOperatorName { get; set; }
			// 6
			[DisplayName("Дата редагування")]
			public DateTime? EditDate { get; set; }
			// 8
			[DisplayName("Вид операції")]
			public string DocumentTypeId { get; set; }
			public string DocumentTypeName { get; set; }
			// 9
			[DisplayName("Склад")]
			public string StockId { get; set; }
			public string StockName { get; set; }
			// 10
			[DisplayName("Тип відправника/одержувача")]
			public string ReceiverTypeId { get; set; }
			public string ReceiverTypeName { get; set; }
			// 11
			[DisplayName("Відправник/одержувач")]
			public string ReceiverId { get; set; }
			public string ReceiverName { get; set; }
			// 12
			[DisplayName("Додаткова аналітика")]
			public string ReceiverAnaliticsId { get; set; }
			public string ReceiverAnaliticsName { get; set; }
			// 13
			[DisplayName("Номенклатура")]
			public string ProductId { get; set; }
			public string ProductName { get; set; }
			// 14
			[DisplayName("Рік врожаю")]
			public string HarvestId { get; set; }
			public string HarvestName { get; set; }
			// 15
			[DisplayName("Брутто")]
			public double? GrossValue { get; set; }
			// 16
			[DisplayName("Тара")]
			public double? TareValue { get; set; }
			// 17
			[DisplayName("Нетто")]
			public double? NetValue { get; set; }
			// 19
			[DisplayName("Водій №1")]
			public Guid DriverOneId { get; set; }
			public string DriverOneName { get; set; }
			// 20
			[DisplayName("Водій №2")]
			public string DriverTwoId { get; set; }
			public string DriverTwoName { get; set; }
			// 21
			[DisplayName("Номер вантажівки")]
			public string TransportId { get; set; }
			// 22
			[DisplayName("Водій найманого автомобіля")]
			public string HiredDriverCode { get; set; }
			// 23
			[DisplayName("Номер найманого автомобіля")]
			public string HiredTansportNumber { get; set; }
			// 25
			[DisplayName("Серія ТТН")]
//			[Required(ErrorMessage = "Поле Серія ТТН повинне бути заповненим.")]
			//[RegularExpression(@"\S+", ErrorMessage = "Хибний формат стрічки")]
			public string IncomeInvoiceSeries { get; set; }
			// 26
			[DisplayName("Номер ТТН")]
//			[Required(ErrorMessage = "Поле Номер ТТН повинне бути заповненим.")]
			//[RegularExpression(@"\S+", ErrorMessage = "Хибний формат стрічки")]
			public string IncomeInvoiceNumber { get; set; }
			// 27
			[DisplayName("Склад контрагента")]
			public string ReceiverDepotId { get; set; }
			// 28
			[DisplayName("Перевізник")]
			public bool IsThirdPartyCarrier { get; set; }
			// 29
			[DisplayName("Перевізник")]
			public string CarrierCode { get; set; }
			// 31
			[DisplayName("Стаття закупки")]
			public string BuyBudgetId { get; set; }
			public string BuyBudgetName { get; set; }
			// 32
			[DisplayName("Стаття реалізації")]
			public string SellBudgetId { get; set; }
			public string SellBudgetName { get; set; }
			// 37
			[DisplayName("Інша тара, кг")]
//			[RegularExpression(@"^(\d{5})$", ErrorMessage = "Не правильно введена вага. Максимум 5 цифр.")]
			public float? PackingWeightValue { get; set; }
			// 38
			[DisplayName("Организація (хранитель)")]
			public string KeeperOrganizationId { get; set; }
			public string KeeperOrganizationName { get; set; }
			// 39
			[DisplayName("Заявка")]
			public string OrderCode { get; set; }
			// 40
			[DisplayName("Код поставки")]
			public string SupplyCode { get; set; }
			// 41
			[DisplayName("Вид надходжень")]
			public string SupplyTypeId { get; set; }
			public string SupplyTypeName { get; set; }
			// 42
			[DisplayName("Час приїзду")]
			public DateTime? RegistrationTime { get; set; }
			// 43
			[DisplayName("Заїзд, дата/час")]
			public DateTime? InTime { get; set; }
			// 44
			[DisplayName("Виїзд, дата/час")]
			public DateTime? OutTime { get; set; }
			// 45
			[DisplayName("Перше зважування брутто, дата/час")]
			public DateTime? FirstGrossTime { get; set; }
			// 46
			[DisplayName("Перше зважування тари, дата/час")]
			public DateTime? FirstTareTime { get; set; }
			// 47
			[DisplayName("Останнє зважування брутто, дата/час")]
			public DateTime? LastGrossTime { get; set; }
			// 48
			[DisplayName("Останнє зважування тари, дата/час")]
			public DateTime? LastTareTime { get; set; }
			// 49
			public Guid? CollectionPointId { get; set; }
			[DisplayName("Пункт прийомки")]
			public string CollectionPointName { get; set; }
			// 51
			[DisplayName("Вологість, клас")]
			public string LabHumidityTypeId { get; set; }
			public string LabHumidityTypeName { get; set; }
			// 52
			[DisplayName("Засміченість, клас")]
			public string LabImpurityTypeId { get; set; }
			public string LabImpurityTypeName { get; set; }
			// 53
			[DisplayName("Зараженість")]
			public bool LabIsInfectioned { get; set; }
			// 54
			[DisplayName("Вологість, значення")]
			public float? LabHumidityValue { get; set; }
			// 55
			[DisplayName("Засміченість, значення")]
			public float? LabImpurityValue { get; set; }
			// 56
			[DisplayName("Вологість по документах у відсотках")]
			public float? DocHumidityValue { get; set; }
			// 57
			[DisplayName("Засміченість по документах у відсотках")]
			public float? DocImpurityValue { get; set; }
			// 58
			[DisplayName("Стороннє Нетто (за документами), кг")]
			public double? DocNetValue { get; set; }
			// 59
			[DisplayName("Дата за документами")]
			public DateTime? DocNetDateTime { get; set; }
			// 61
			public Guid? ReturnCauseId { get; set; }
			[DisplayName("Причина повернення")]
			public string ReturnCauseName { get; set; }
			// 62
			[DisplayName("Номер причепа")]
			public string TrailerId { get; set; }
			// 63
			[DisplayName("Номер найманого причепа")]
			public string TrailerNumber { get; set; }
			// 64
			[DisplayName("Маршрутний лист, номер")]
			public string TripTicketNumber { get; set; }
			// 65
			[DisplayName("Маршрутний лист, дата")]
			public DateTime? TripTicketDateTime { get; set; }
			// 66
			[DisplayName("Серія доручення")]
			public string WarrantSeries { get; set; }
			// 67
			[DisplayName("Номер доручення")]
			public string WarrantNumber { get; set; }
			// 68
			[DisplayName("Дата доручення")]
			public DateTime? WarrantDatetime { get; set; }
			// 69
			[DisplayName("Доручення через")]
			public string WarrantManagerName { get; set; }
			// 70
			[DisplayName("Список пломб")]
			public string StampList { get; set; }
			// 71
			[DisplayName("Статус ТТН")]
			public string StatusType { get; set; }
			// 73
			[DisplayName("Номер наказу")]
			public string RuleNumber { get; set; }
			// 74
			[DisplayName("Брутто причепу")]
			public double? TrailerGrossValue { get; set; }
			// 75
			[DisplayName("Тара причепу")]
			public double? TrailerTareValue { get; set; }
			// 76
			[DisplayName("Стороннє Брутто (За документами), кг")]
//			[RegularExpression(@"^(\d{5})$", ErrorMessage = "Не правильно введена вага. Максимум 5 цифр.")]
			public double? IncomeDocGrossValue { get; set; }
			// 77
			[DisplayName("Стороння Тара (За документами), кг")]
//			[RegularExpression(@"^(\d{5})$", ErrorMessage = "Не правильно введена вага. Максимум 5 цифр.")]
			public double? IncomeDocTareValue { get; set; }
			// 78
			[DisplayName("Дата відправника")]
			public DateTime? IncomeDocDateTime { get; set; }
			// 79
			[DataType(DataType.MultilineText)]
			[DisplayName("Коментар")]
			public string Comments { get; set; }
			// 80
			[DisplayName("Дельта зважування")]
			public double? WeightDeltaValue { get; set; }
			// 81
			[DisplayName("Вид поставки")]
			public string SupplyTransportTypeId { get; set; }
			// 82
			[DisplayName("Лаборант")]
			public string LabolatoryOperatorName { get; set; }
			// 83
			[DisplayName("Брутто, оператор")]
			public string GrossOperatorName { get; set; }
			// 86
			[DisplayName("В'їзд, платформа")]
			public long ScaleInNumber { get; set; }
			// 87
			[DisplayName("Виїзд, платформа")]
			public long ScaleOutNumber { get; set; }
			// 92
			[DisplayName("Номер партії")]
			public string BatchNumber { get; set; }
			// 93
			[DisplayName("Тара, оператор")]
			public string TareOperatorName { get; set; }
			// 101
			[DisplayName("Погрузка, оператор")]
			public string LoadingOperatorName { get; set; }
			// 102
			[DisplayName("Дата розгрузки")]
			public DateTime? LoadOutDateTime { get; set; }
			// 103
			[DisplayName("Маршрут перевізника")]
			public string CarrierRouteName { get; set; }
			public Guid? CarrierRouteId { get; set; }
			// 104
			[DisplayName("Масличність/Протеїн")]
			public float? LabOilContentValue { get; set; }
			// 106
			[DisplayName("Номер документу 1С (GUID)")]
			public string DeliveryBillId { get; set; }
			// 105
			[DisplayName("Номер документу 1С")]
			public string DeliveryBillCode { get; set; }
			// 107
			[DisplayName("Інформація про перевізника")]
			public string InformationCarrier { get; set; }
			// 108
			[DisplayName("Плюс")]
			public int LoadTargetDeviationPlus { get; set; }
			// 109
			[DisplayName("Плюс")]
			public int PackingWeightDeviationPlus { get; set; }
			// 110
			[DisplayName("Мінус")]
			public int LoadTargetDeviationMinus { get; set; }
			// 111
			[DisplayName("Мінус")]
			public int PackingWeightDeviationMinus { get; set; }
			// 112
			public long? LabFileId { get; set; }
            // 113
		    [DisplayName("Ім'я стороннього перевізника")]
            public string CustomPartnerName { get; set; }

            public Guid? CarrierId { get; set; }

			[DisplayName("Технологічний Маршрут")]
			public bool IsTechnologicalRoute { get; set; }

			[DisplayName("Інформувати про реєстрацію")]
			public List<InformEmployeeVm> OnRegisterInformEmployees { get; set; }
			[DisplayName("Договір перевізника")]
			public string ContractCarrier { get; set; }
			public bool IsPreRegistered { get; set; }

            public ProductContentListVm ProductContentList { get; set; }
		}
	}
}