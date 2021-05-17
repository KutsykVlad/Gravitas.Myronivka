using System.Data.Entity.Migrations;
using Gravitas.Model;
using Dom = Gravitas.Model.DomainValue.Dom;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.DAL.TestData {

	public static partial class TestData
	{
		public static void ExternalData(GravitasDbContext context) {
			context.Set<ExternalData.DeliveryBillType>().AddOrUpdate(
				new ExternalData.DeliveryBillType { Id = Dom.ExternalData.DeliveryBill.Type.Incoming, Name = @"Приход" });
			context.Set<ExternalData.DeliveryBillType>().AddOrUpdate(
				new ExternalData.DeliveryBillType { Id = Dom.ExternalData.DeliveryBill.Type.Outgoing, Name = @"Расход" });

			context.Set<ExternalData.DeliveryBillStatus>().AddOrUpdate(
				new ExternalData.DeliveryBillStatus() { Id = Dom.ExternalData.DeliveryBill.Status.Security, Name = @"Охрана" });
			context.Set<ExternalData.DeliveryBillStatus>().AddOrUpdate(
				new ExternalData.DeliveryBillStatus() { Id = Dom.ExternalData.DeliveryBill.Status.Labolatory, Name = @"Визировка" });
			context.Set<ExternalData.DeliveryBillStatus>().AddOrUpdate(
				new ExternalData.DeliveryBillStatus() { Id = Dom.ExternalData.DeliveryBill.Status.Weightbridge, Name = @"Весовая" });
			context.Set<ExternalData.DeliveryBillStatus>().AddOrUpdate(
				new ExternalData.DeliveryBillStatus() { Id = Dom.ExternalData.DeliveryBill.Status.Finished, Name = @"Закрыт" });

			context.Set<ExternalData.OriginType>().AddOrUpdate(
				new ExternalData.OriginType() { Id = Dom.ExternalData.Origin.Type.Contractor, Name = @"Контрагент" });
			context.Set<ExternalData.OriginType>().AddOrUpdate(
				new ExternalData.OriginType() { Id = Dom.ExternalData.Origin.Type.Stock, Name = @"Склад" });
			context.Set<ExternalData.OriginType>().AddOrUpdate(
				new ExternalData.OriginType() { Id = Dom.ExternalData.Origin.Type.Subdivision, Name = @"Подразделение" });

			context.Set<ExternalData.SupplyTransportType>().AddOrUpdate(
				new ExternalData.SupplyTransportType() { Id = Dom.ExternalData.SupplyTransportType.ByTruck, Name = @"Авто" });
			context.Set<ExternalData.SupplyTransportType>().AddOrUpdate(
				new ExternalData.SupplyTransportType() { Id = Dom.ExternalData.SupplyTransportType.ByWagon, Name = @"ЖД" });

			context.Set<ExternalData.SupplyType>().AddOrUpdate(
				new ExternalData.SupplyType() { Id = Dom.ExternalData.SupplyType.Sell, Name = @"Реализация" });
			context.Set<ExternalData.SupplyType>().AddOrUpdate(
				new ExternalData.SupplyType() { Id = Dom.ExternalData.SupplyType.Buy, Name = @"Покупка" });
			context.Set<ExternalData.SupplyType>().AddOrUpdate(
				new ExternalData.SupplyType() { Id = Dom.ExternalData.SupplyType.Storage, Name = @"Хранение" });
			context.Set<ExternalData.SupplyType>().AddOrUpdate(
				new ExternalData.SupplyType() { Id = Dom.ExternalData.SupplyType.ProcessingContract, Name = @"Давальческая схема" });

			context.Set<ExternalData.LabHumidityСlassifier>().AddOrUpdate(
				new ExternalData.LabHumidityСlassifier() { Id = Dom.ExternalData.LabClassifier.Humidity.Class1, Name = @"Влажное" });
			context.Set<ExternalData.LabHumidityСlassifier>().AddOrUpdate(
				new ExternalData.LabHumidityСlassifier() { Id = Dom.ExternalData.LabClassifier.Humidity.Class2, Name = @"Мокрое" });
			context.Set<ExternalData.LabHumidityСlassifier>().AddOrUpdate(
				new ExternalData.LabHumidityСlassifier() { Id = Dom.ExternalData.LabClassifier.Humidity.Class3, Name = @"Среднее" });
			context.Set<ExternalData.LabHumidityСlassifier>().AddOrUpdate(
				new ExternalData.LabHumidityСlassifier() { Id = Dom.ExternalData.LabClassifier.Humidity.Class4, Name = @"Сухое" });
			context.Set<ExternalData.LabHumidityСlassifier>().AddOrUpdate(
				new ExternalData.LabHumidityСlassifier() { Id = Dom.ExternalData.LabClassifier.Humidity.Class5, Name = @"Сырое" });

			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class1, Name = @"Сорное" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class2, Name = @"Чистое" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class3, Name = @"СреднейСорности" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class4, Name = @"СорноеНизкаяМасличность" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class5, Name = @"ЧистоеНизкаяМасличность" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class6, Name = @"СреднейСорностиНизкаяМасличность" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class7, Name = @"СорноеНизкийПротеин" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class8, Name = @"ЧистоеНизкийПротеин" });
			context.Set<ExternalData.LabImpurityСlassifier>().AddOrUpdate(
				new ExternalData.LabImpurityСlassifier() { Id = Dom.ExternalData.LabClassifier.Impurity.Class9, Name = @"СреднейСорностиНизкийПротеин" });

			context.Set<ExternalData.LabInfectionedСlassifier>().AddOrUpdate(
				new ExternalData.LabInfectionedСlassifier() { Id = Dom.ExternalData.LabClassifier.Infection.Class1, Name = @"Да" });
			context.Set<ExternalData.LabInfectionedСlassifier>().AddOrUpdate(
				new ExternalData.LabInfectionedСlassifier() { Id = Dom.ExternalData.LabClassifier.Infection.Class2, Name = @"Нет" });

			context.Set<ExternalData.LabDeviceResultType>().AddOrUpdate(
				new ExternalData.LabDeviceResultType() { Id = Dom.ExternalData.LabDevResultType.Ignore, Name = @"Не використовувати" });
			context.Set<ExternalData.LabDeviceResultType>().AddOrUpdate(
				new ExternalData.LabDeviceResultType() { Id = Dom.ExternalData.LabDevResultType.SaveAsComment, Name = @"В коментар" });
			context.Set<ExternalData.LabDeviceResultType>().AddOrUpdate(
				new ExternalData.LabDeviceResultType() { Id = Dom.ExternalData.LabDevResultType.SaveAsHumidity, Name = @"Вологість" });
			context.Set<ExternalData.LabDeviceResultType>().AddOrUpdate(
				new ExternalData.LabDeviceResultType() { Id = Dom.ExternalData.LabDevResultType.SaveAsEffectiveValue, Name = @"Масличність" });
			
			context.SaveChanges();
		}
	}
}
