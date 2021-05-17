using Gravitas.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.DAL.Mapping
{
    class PriorityMap: BaseEntityMap<QueueItemPriority, long>
    {
        public PriorityMap()
        {
            this.ToTable("QueueItemPriority");

            this.Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);

            
        }
    }
}
