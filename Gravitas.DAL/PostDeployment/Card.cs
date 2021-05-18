using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment {

	public static partial class PostDeployment {
		public static class Card {

			public static void Type(GravitasDbContext context) {
				context.Set<CardType>().AddOrUpdate(new CardType() {Id = Dom.Card.Type.EmployeeCard, Name = "Картка працівника"});
				context.Set<CardType>().AddOrUpdate(new CardType() {Id = Dom.Card.Type.TicketCard, Name = "Картка перевізника"});
				context.Set<CardType>().AddOrUpdate(new CardType() {Id = Dom.Card.Type.TransportCard, Name = "Картка транспорту (125Гц)"});
				context.Set<CardType>().AddOrUpdate(new CardType() {Id = Dom.Card.Type.LaboratoryTray, Name = "Лоток лабораторії"});

				context.SaveChanges();
			}
		}
	}
}
