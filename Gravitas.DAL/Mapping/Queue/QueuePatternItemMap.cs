using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping
{
    class QueuePatternItemMap : EntityTypeConfiguration<Gravitas.Model.QueuePatternItem>
    {
        public QueuePatternItemMap()
        {
            this.ToTable("QueuePatternItem");

            this.Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(t => t.QueueItemCategory)
                .WithMany(t => t.QueuePatternItemsSet)
                .HasForeignKey(t=>t.CategoryId);

            this.HasRequired(t => t.QueueItemPriority)
                .WithMany(t => t.Items)
                .HasForeignKey(t=>t.PriorityId);

            this.HasOptional(t => t.Partner)
                .WithMany(t => t.QueuePatternItemsSet)
                .HasForeignKey(t => t.PartnerId);
        }
    }
}
