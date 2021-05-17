using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class TicketMap : EntityTypeConfiguration<Ticket> {

		public TicketMap() {
			ToTable("Ticket");

			HasIndex(e => new { e.ContainerId, e.OrderNo })
				.HasName("IX_ContainerId_OrderNo").IsUnique();

			Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			HasRequired(e => e.TicketStatus)
				.WithMany(e => e.TicketSet)
				.HasForeignKey(e => e.StatusId);
//			
//			HasRequired(e => e.Node)
//				.WithMany(e => e.TicketSet)
//				.HasForeignKey(e => e.NodeId);
		}
	}
}
