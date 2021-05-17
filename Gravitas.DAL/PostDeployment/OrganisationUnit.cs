using System.Data.Entity.Migrations;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OrganizationUnit.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment {

	public static partial class PostDeployment {
		public static class OrganizationUnit {

			public static void Type(GravitasDbContext context) {
				context.Set<OrganizationUnitType>().AddOrUpdate(new OrganizationUnitType() { Id = Dom.OrganizationUnit.Type.Organization.Id, Name = "Організація" });
				context.Set<OrganizationUnitType>().AddOrUpdate(new OrganizationUnitType() { Id = Dom.OrganizationUnit.Type.Plant.Id, Name = "Підприємство" });
				context.Set<OrganizationUnitType>().AddOrUpdate(new OrganizationUnitType() { Id = Dom.OrganizationUnit.Type.Department.Id, Name = "Департамент" });
				context.Set<OrganizationUnitType>().AddOrUpdate(new OrganizationUnitType() { Id = Dom.OrganizationUnit.Type.Area.Id, Name = "Теритоія" });
				context.Set<OrganizationUnitType>().AddOrUpdate(new OrganizationUnitType() { Id = Dom.OrganizationUnit.Type.Sector.Id, Name = "Сектор" });
				context.Set<OrganizationUnitType>().AddOrUpdate(new OrganizationUnitType() { Id = Dom.OrganizationUnit.Type.Workstantion.Id, Name = "Робоча станція" });
				context.SaveChanges();
			}
			
			public static void Data(GravitasDbContext context) {
//				context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Organization.MHP, Name = "МХП", UnitTypeId = Dom.OrganizationUnit.Type.Organization.Id });
//				context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Plant.KKZ, Name = "ККЗ", UnitTypeId = Dom.OrganizationUnit.Type.Plant.Id });
//				context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Area.MainArea, Name = "Основна територія", UnitTypeId = Dom.OrganizationUnit.Type.Area.Id });
//				context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Area.UpperArea, Name = "Верхня територія", UnitTypeId = Dom.OrganizationUnit.Type.Area.Id });
//				context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Area.CarFleet, Name = "Автопарк", UnitTypeId = Dom.OrganizationUnit.Type.Area.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.Elevator1, Name = "Елеватор №1", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.Elevator2, Name = "Елеватор №2", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.Elevator3, Name = "Елеватор №3", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5, Name = "Елеватор №4 та 5", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.TareWarehouse, Name = "Склад таріровки", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.OilLoad, Name = "Погрузка Олії", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.CustomWarehouse, Name = "Митний склад", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.MixedFeedLoad, Name = "Погрузка Комбікорму", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				context.Set<Model.DomainModel.OrganizationUnit.DAO.OrganizationUnit>().AddOrUpdate(new Model.DomainModel.OrganizationUnit.DAO.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.Husk, Name = "Нижня територія", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.ShrotLoad, Name = "Завантаження шрота", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.Stores, Name = "Склади", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				//context.Set<Model.OrganizationUnit>().AddOrUpdate(new Model.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.MPZ, Name = "МПЗ", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				context.Set<Model.DomainModel.OrganizationUnit.DAO.OrganizationUnit>().AddOrUpdate(new Model.DomainModel.OrganizationUnit.DAO.OrganizationUnit { Id = Dom.OrganizationUnit.Type.Workstantion.TMC, Name = "ТМЦ", UnitTypeId = Dom.OrganizationUnit.Type.Workstantion.Id });
				
				context.SaveChanges();
			}
		}
	}
}
