using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.DAL.Mapping
{
    internal class BaseEntityMap<TEntity, TKey> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
    {
        public BaseEntityMap()
        {
            HasKey(t => t.Id);
        }
    }
}