using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.TestData
{
    public static partial class TestData
    {
        public static void QueuePatternItems(GravitasDbContext context)
        {
            context.Set<QueuePatternItem>().AddOrUpdate(new QueuePatternItem {Id = 1, CategoryId = Dom.Queue.Category.Company, Count = 1, PartnerId = null, PriorityId = Dom.Queue.Priority.High});
            context.Set<QueuePatternItem>().AddOrUpdate(new QueuePatternItem { Id = 82, CategoryId = Dom.Queue.Category.Others, Count = 2, PartnerId = null, PriorityId = Dom.Queue.Priority.Low });
        }
    }
}
