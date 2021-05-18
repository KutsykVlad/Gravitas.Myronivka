using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL.Mapping.Queue
{
    class QueuePatternItemMap : EntityTypeConfiguration<QueuePatternItem>
    {
        public QueuePatternItemMap()
        {
            ToTable("QueuePatternItem");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.QueueItemCategory)
                .WithMany(t => t.QueuePatternItemsSet)
                .HasForeignKey(t=>t.CategoryId);

            HasRequired(t => t.QueueItemPriority)
                .WithMany(t => t.Items)
                .HasForeignKey(t=>t.PriorityId);

            HasOptional(t => t.Partner)
                .WithMany(t => t.QueuePatternItemsSet)
                .HasForeignKey(t => t.PartnerId);
        }
    }
}
