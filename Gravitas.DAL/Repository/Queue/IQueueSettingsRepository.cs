using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Queue
{
    public interface IQueueSettingsRepository: IBaseRepository
    {
        IQueryable<QueuePatternItem> GetQueuePatternItems();
        List<string> GetPriorities();
        List<string> GetCategories();
    }
}
