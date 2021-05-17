using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL
{
    public interface IQueueSettingsRepository: IBaseRepository<GravitasDbContext>
    {
        IQueryable<QueuePatternItem> GetQueuePatternItems();
        IQueryable<QueueItemPriority> GetPriorities();
        IQueryable<QueueItemCategory> GetCategories();
    }
}
