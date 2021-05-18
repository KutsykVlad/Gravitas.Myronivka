using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.DAL.Mapping._Base
{
    internal class BaseEntityMap<TEntity, TKey> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
    {
        public BaseEntityMap()
        {
            HasKey(t => t.Id);
        }
    }
}