using System.Data.Entity.Migrations;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment {

    public static partial class PostDeployment {
        public static class TicketFile {
            public static void Type(GravitasDbContext context) {

                context.Set<TicketFileType>().AddOrUpdate(new TicketFileType
                    {Id = Dom.TicketFile.Type.CustomerLabCertificate, Name = "Сертифікат лабораторії клієнта"});
                context.Set<TicketFileType>().AddOrUpdate(new TicketFileType
                    {Id = Dom.TicketFile.Type.LabCertificate, Name = "Сертифікат лабораторії"});
                context.Set<TicketFileType>().AddOrUpdate(new TicketFileType
                    {Id = Dom.TicketFile.Type.Other, Name = "Інше"});
                context.Set<TicketFileType>().AddOrUpdate(new TicketFileType
                    { Id = Dom.TicketFile.Type.CentralLabCertificate, Name = "Сертифікат центральної лабораторії" });
                context.SaveChanges();
            }
        }
    }
}