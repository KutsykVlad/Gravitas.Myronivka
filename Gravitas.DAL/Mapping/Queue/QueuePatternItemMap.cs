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
            
            Property(e => e.CategoryId)
                .IsRequired();
            
            Property(e => e.PriorityId)
                .IsRequired();

            HasOptional(t => t.Partner)
                .WithMany(t => t.QueuePatternItemsSet)
                .HasForeignKey(t => t.PartnerId);
        }
    }
}
