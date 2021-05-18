using System.Linq;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL.Repository.Queue
{
    public interface IQueueSettingsRepository: IBaseRepository
    {
        IQueryable<QueuePatternItem> GetQueuePatternItems();
        IQueryable<QueueItemPriority> GetPriorities();
        IQueryable<QueueItemCategory> GetCategories();
    }
}
