using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment {

	public static partial class PostDeployment {
		public static class Ticket {

			public static void Status(GravitasDbContext context) {
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.New, Name = "Новий" });
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.Blank, Name = "Бланк" });
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.ToBeProcessed, Name = "До опрацювання" });
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.Processing, Name = "В роботі" });
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.Completed, Name = "Завершено" });
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.Closed, Name = "Проведено" });
				context.Set<TicketStatus>().AddOrUpdate(new TicketStatus() { Id = Dom.Ticket.Status.Canceled, Name = "Скасовано" });
				context.SaveChanges();
			}
		}
	}
}
