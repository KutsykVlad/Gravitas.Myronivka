using System.Data.Entity.Migrations;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpRoutine.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment {

	public static partial class PostDeployment {

		public static class OpRoutine {

			public static void Items(GravitasDbContext context) {
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.SingleWindow.Id, Name = "Єдине вікно" });
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.SecurityIn.Id, Name = "КПП /Заїзд/"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.SecurityOut.Id, Name = "КПП /Виїзд/" });
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.SecurityReview.Id, Name = "КПП /Точка оглядова/" });
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.LabolatoryIn.Id, Name = "Візіровка" });
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.LabolatoryOut.Id, Name = "Лабораторія" });
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.Weighbridge.Id, Name = "Вагова"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.UnloadPointGuide.Id, Name = "Вигрузка. Гід"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.UnloadPointGuide2.Id, Name = "Вигрузка2. Гід"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.UnloadPointType1.Id, Name = "Вигрузка. Тип 1"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.UnloadPointType2.Id, Name = "Вигрузка. Тип 2" });
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.LoadPointType1.Id, Name = "Загрузка. Тип 2"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.MixedFeedManage.Id, Name = "ККЗ. Редагування показників."});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.LoadCheckPoint.Id, Name = "Загрузка. Чекпойнт"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.LoadPointGuide.Id, Name = "Загрузка. Гід"});
				context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.LoadPointGuide2.Id, Name = "Загрузка2. Гід"});
			    context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.CentralLaboratorySamples.Id, Name = "Центральна лабораторія / Відбір проб" });
			    context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.CentralLaboratoryProcess.Id, Name = "Центральна лабораторія" });
			    context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.MixedFeedGuide.Id, Name = "Комбікормовий завод / Призначення" });
			    context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.MixedFeedLoad.Id, Name = "Комбікормовий завод / Завантаження" });
			    context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutine>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutine { Id = Dom.OpRoutine.UnloadCheckPoint.Id, Name = "Розвантаження. Чекпойнт" });
                context.SaveChanges();
			}

			public static void Processor(GravitasDbContext context) {
				context.Set<OpRoutineProcessor>().AddOrUpdate(new OpRoutineProcessor() { Id = Dom.OpRoutine.Processor.CoreService, Name = "Core Service" });
				context.Set<OpRoutineProcessor>().AddOrUpdate(new OpRoutineProcessor() { Id = Dom.OpRoutine.Processor.WebUI, Name = "Web UI" });
				context.SaveChanges();
			}
		}
	}
}
