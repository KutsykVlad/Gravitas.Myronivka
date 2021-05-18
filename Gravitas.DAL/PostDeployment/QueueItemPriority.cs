using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment
{
   
        public static partial class PostDeployment
        {
            public static class QueueItemPriority
            {
                public static void PriorityType(GravitasDbContext context)
                {
                    context.Set<Model.DomainModel.Queue.DAO.QueueItemPriority>().AddOrUpdate(new Model.DomainModel.Queue.DAO.QueueItemPriority { Id = Dom.Queue.Priority.High, Description = "Високий" });
                    context.Set<Model.DomainModel.Queue.DAO.QueueItemPriority>().AddOrUpdate(new Model.DomainModel.Queue.DAO.QueueItemPriority { Id = Dom.Queue.Priority.Medium, Description = "Середній" });
                    context.Set<Model.DomainModel.Queue.DAO.QueueItemPriority>().AddOrUpdate(new Model.DomainModel.Queue.DAO.QueueItemPriority { Id = Dom.Queue.Priority.Low, Description = "Низький" });

                    context.SaveChanges();
                }
            }
        }
    
}
