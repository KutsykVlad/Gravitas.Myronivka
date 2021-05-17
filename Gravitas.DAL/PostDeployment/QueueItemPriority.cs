using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment
{
   
        public static partial class PostDeployment
        {
            public static class QueueItemPriority
            {
                public static void PriorityType(GravitasDbContext context)
                {
                    context.Set<Model.QueueItemPriority>().AddOrUpdate(new Model.QueueItemPriority { Id = Dom.Queue.Priority.High, Description = "Високий" });
                    context.Set<Model.QueueItemPriority>().AddOrUpdate(new Model.QueueItemPriority { Id = Dom.Queue.Priority.Medium, Description = "Середній" });
                    context.Set<Model.QueueItemPriority>().AddOrUpdate(new Model.QueueItemPriority { Id = Dom.Queue.Priority.Low, Description = "Низький" });

                    context.SaveChanges();
                }
            }
        }
    
}
