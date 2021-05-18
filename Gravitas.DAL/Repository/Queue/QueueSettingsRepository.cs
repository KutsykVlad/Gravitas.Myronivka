using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.DAL.Repository.Queue;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL
{
    public class QueueSettingsRepository : BaseRepository, IQueueSettingsRepository
    {
        private readonly GravitasDbContext _context;

        public QueueSettingsRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<QueuePatternItem> GetQueuePatternItems()
        {
            return GetQuery<QueuePatternItem, int>();
        }

        public IQueryable<QueueItemPriority> GetPriorities()
        {
            return GetQuery<QueueItemPriority, int>();
        }

        public IQueryable<QueueItemCategory> GetCategories()
        {
            return GetQuery<QueueItemCategory, int>();
        }
    }
}