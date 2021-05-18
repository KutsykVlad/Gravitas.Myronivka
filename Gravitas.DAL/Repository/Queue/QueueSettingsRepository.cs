using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL
{
    public class QueueSettingsRepository : BaseRepository<GravitasDbContext>, IQueueSettingsRepository
    {
        private readonly GravitasDbContext _context;

        public QueueSettingsRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<QueuePatternItem> GetQueuePatternItems()
        {
            return GetQuery<QueuePatternItem, long>();
        }

        public IQueryable<QueueItemPriority> GetPriorities()
        {
            return GetQuery<QueueItemPriority, long>();
        }

        public IQueryable<QueueItemCategory> GetCategories()
        {
            return GetQuery<QueueItemCategory, long>();
        }
    }
}