using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.DAL.Repository._Base
{
    public class BaseRepository : IBaseRepository
    {
        private readonly GravitasDbContext _context;

        public BaseRepository(GravitasDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetQuery<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public TEntity GetSingleOrDefault<TEntity, TKey>(Func<TEntity, bool> predicate, bool asTracking = false) where TEntity : BaseEntity<TKey>
        {
            if (asTracking)
            {
                return _context.Set<TEntity>().SingleOrDefault(predicate);
            }
            else
            {
                return _context.Set<TEntity>().AsNoTracking().SingleOrDefault(predicate);
            }
        }

        public TEntity GetFirstOrDefault<TEntity, TKey>(Func<TEntity, bool> predicate) where TEntity : BaseEntity<TKey>
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public void Add<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();
        }

        public void Update<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dbItem = _context.Set<TEntity>().Find(item.Id);
            if (dbItem == null) throw new DbEntityValidationException();

            DbEntityEntry entry = _context.Entry(dbItem);
            entry.CurrentValues.SetValues(item);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddOrUpdate<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dbItem = _context.Set<TEntity>().Find(item.Id);
            if (dbItem == null)
            {
                _context.Set<TEntity>().Add(item);
            }
            else
            {
                DbEntityEntry entry = _context.Entry(dbItem);
                entry.CurrentValues.SetValues(item);
                entry.State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public void AddOrUpdateWithoutSaveChanges<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dbItem = _context.Set<TEntity>().Find(item.Id);
            if (dbItem == null)
            {
                _context.Set<TEntity>().Add(item);
            }
            else
            {
                DbEntityEntry entry = _context.Entry(dbItem);
                entry.CurrentValues.SetValues(item);
                entry.State = EntityState.Modified;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Delete<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dbItem = _context.Set<TEntity>().Find(item.Id);
            _context.Set<TEntity>().Attach(dbItem);
            _context.Set<TEntity>().Remove(dbItem);
            _context.SaveChanges();
        }
    }
}