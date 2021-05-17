namespace Gravitas.Model {

	public static partial class Dom {

		public static class ExternalData {

			public static class DeliveryBill {
				public static class Type {
					public const string Incoming = @"Приход";
					public const string Outgoing = @"Расход";
				}

				public static class Status {
					public const string Security = @"Охрана";
					public const string Labolatory = @"Визировка";
					public const string Weightbridge = @"Весовая";
					public const string Finished = @"Закрыт";
				}
			}

			public static class Origin {
				public static class Type {
					public const string Contractor = @"Контрагент";
					public const string Stock = @"Склад";
					public const string Subdivision = "Подразделение";
				}
			}

			public static class SupplyTransportType {
				public const string ByTruck = @"Авто";
				public const string ByWagon = @"ЖД";
			}

			public static class SupplyType {

				public const string Sell = @"Реализация";
				public const string Buy = @"Покупка";
				public const string Storage = @"Хранение";
				public const string ProcessingContract = @"Давальческая схема";
			}

			public static class LabClassifier {
				public static class Humidity {
					public const string Class1 = @"Влажное";
					public const string Class2 = @"Мокрое";
					public const string Class3 = @"Среднее";
					public const string Class4 = @"Сухое";
					public const string Class5 = @"Сырое";
				}

				public static class Impurity {
					public const string Class1 = @"Сорное";
					public const string Class2 = @"Чистое";
					public const string Class3 = @"СреднейСорности";
					public const string Class4 = @"СорноеНизкаяМасличность";
					public const string Class5 = @"ЧистоеНизкаяМасличность";
					public const string Class6 = @"СреднейСорностиНизкаяМасличность";
					public const string Class7 = @"СорноеНизкийПротеин";
					public const string Class8 = @"ЧистоеНизкийПротеин";
					public const string Class9 = @"СреднейСорностиНизкийПротеин";
				}

				public static class Infection {
					public const string Class1 = @"Да";
					public const string Class2 = @"Нет";
				}
			}

			public static class LabDevResultType {
				public const string Ignore = @"Ignore";
				public const string SaveAsComment = @"SaveAsComment";
				public const string SaveAsHumidity = @"SaveAsHumidity";
				public const string SaveAsEffectiveValue = @"SaveAsEffectiveValue";
			}
		}
	}
}
