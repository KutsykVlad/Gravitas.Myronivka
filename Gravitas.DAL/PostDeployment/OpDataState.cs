using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.PostDeployment
{
	public static partial class PostDeployment {

		public static class OpDataState {

			public static void Items(GravitasDbContext context) {

				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Init, Name = "Бланк"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Processing, Name = "В обробці"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Collision, Name = "На погодженні"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.CollisionApproved, Name = "Погодженно"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.CollisionDisapproved, Name = "Відмовлено у погодженні"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Rejected, Name = "Відмовлено"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Canceled, Name = "Скасовано"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Processed, Name = "Виконано"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.PartLoad, Name = "Часткове завантаження"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.PartUnload, Name = "Часткове розвантаження"});
				context.Set<Model.DomainModel.OpDataState.DAO.OpDataState>().AddOrUpdate(new Model.DomainModel.OpDataState.DAO.OpDataState{Id = Dom.OpDataState.Reload, Name = "Перезавантаження"});

				context.SaveChanges();
			}
		}
	}
}
