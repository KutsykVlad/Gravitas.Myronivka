using Gravitas.Model;
using System;
using System.Linq;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.DAL
{
    public interface IBaseRepository<TContext> where TContext : BaseDbContext<TContext>
    {
        TEntity GetEntity<TEntity, TKey>(TKey id) where TEntity : BaseEntity<TKey>;
        IQueryable<TEntity> GetQuery<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        // IQueryable<TEntity> GetQuery<TEntity, TKey>(Func<TEntity, bool> predicate) where TEntity : BaseEntity<TKey>;

        TEntity GetSingleOrDefault<TEntity, TKey>(Func<TEntity, bool> predicate, bool asTracking = false) where TEntity : BaseEntity<TKey>;
        TEntity GetFirstOrDefault<TEntity, TKey>(Func<TEntity, bool> predicate) where TEntity : BaseEntity<TKey>;

        void Add<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>;
        void Update<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>;
        void AddOrUpdate<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>;
        void AddOrUpdateWithoutSaveChanges<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>;
        void Delete<TEntity, TKey>(TEntity item) where TEntity : BaseEntity<TKey>;
        void Save();
    }
}
