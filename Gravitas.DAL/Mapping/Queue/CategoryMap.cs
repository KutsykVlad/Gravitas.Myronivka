using Gravitas.Model;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL.Mapping
{
    class CategoryMap : BaseEntityMap<QueueItemCategory, long>
    {
        public CategoryMap()
        {
            this.ToTable("QueueItemCategory");

            this.Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
