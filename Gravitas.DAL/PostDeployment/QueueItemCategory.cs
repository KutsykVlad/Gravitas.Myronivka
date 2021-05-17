using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class QueueItemCategory
        {
            public static void CategoryType(GravitasDbContext context)
            {
                context.Set<Model.QueueItemCategory>().AddOrUpdate(new Model.QueueItemCategory { Id = Dom.Queue.Category.Company, Description = "Свої" });
                context.Set<Model.QueueItemCategory>().AddOrUpdate(new Model.QueueItemCategory { Id = Dom.Queue.Category.Partners, Description = "Партнери" });
                context.Set<Model.QueueItemCategory>().AddOrUpdate(new Model.QueueItemCategory { Id = Dom.Queue.Category.Others, Description = "Інші" });
                context.Set<Model.QueueItemCategory>().AddOrUpdate(new Model.QueueItemCategory { Id = Dom.Queue.Category.MixedFeedLoad, Description = "Завантаження комбікорму" });
                context.Set<Model.QueueItemCategory>().AddOrUpdate(new Model.QueueItemCategory { Id = Dom.Queue.Category.PreRegisterCategory, Description = "Попередня реєстрація" });

                context.SaveChanges();
            }
        }
    }
}
