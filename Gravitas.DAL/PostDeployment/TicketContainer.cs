using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment {

	public static partial class PostDeployment {
		public static class TicketContainer {

			public static void Status(GravitasDbContext context) {
				context.Set<TicketContainerStatus>().AddOrUpdate(new TicketContainerStatus() { Id = Dom.TicketContainer.Status.Active, Name = "Активний" });
				context.Set<TicketContainerStatus>().AddOrUpdate(new TicketContainerStatus() { Id = Dom.TicketContainer.Status.Inactive, Name = "Не активний" });
				context.SaveChanges();
			}
		}
	}
}
