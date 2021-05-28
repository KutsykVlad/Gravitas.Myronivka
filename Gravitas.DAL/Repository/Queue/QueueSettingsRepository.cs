using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Queue
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

        public List<string> GetPriorities()
        {
            var result = new List<string>();
            var names = Enum.GetNames(typeof(QueueItemPriority));
            foreach (var name in names)
            {
                var field = typeof(QueueItemPriority).GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                result.AddRange(from DescriptionAttribute fd in fds select fd.Description);
            }
            return result;
        }

        public List<string> GetCategories()
        {
            var result = new List<string>();
            var names = Enum.GetNames(typeof(QueueItemCategory));
            foreach (var name in names)
            {
                var field = typeof(QueueItemCategory).GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                result.AddRange(from DescriptionAttribute fd in fds select fd.Description);
            }
            return result;
        }
    }
}