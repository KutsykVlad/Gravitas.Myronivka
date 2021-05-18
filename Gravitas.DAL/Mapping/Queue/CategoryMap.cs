using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL.Mapping.Queue
{
    class CategoryMap : BaseEntityMap<QueueItemCategory, int>
    {
        public CategoryMap()
        {
            ToTable("QueueItemCategory");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
