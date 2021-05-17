using System.Data.Entity.Migrations;

namespace Gravitas.DAL.PostDeployment
{
	public static partial class PostDeployment {

		public static class OpDataState {

			public static void Items(GravitasDbContext context) {

				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Init, Name = "Бланк"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Processing, Name = "В обробці"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Collision, Name = "На погодженні"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.CollisionApproved, Name = "Погодженно"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.CollisionDisapproved, Name = "Відмовлено у погодженні"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Rejected, Name = "Відмовлено"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Canceled, Name = "Скасовано"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Processed, Name = "Виконано"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.PartLoad, Name = "Часткове завантаження"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.PartUnload, Name = "Часткове розвантаження"});
				context.Set<Model.OpDataState>().AddOrUpdate(new Model.OpDataState{Id = Model.Dom.OpDataState.Reload, Name = "Перезавантаження"});

				context.SaveChanges();
			}
		}
	}
}
