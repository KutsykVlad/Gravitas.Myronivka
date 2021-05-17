using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class TicketConteinerMap : EntityTypeConfiguration<TicketContainer> {

		public TicketConteinerMap() {
			this.ToTable("TicketConteiner");

			this.Property(p => p.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(e => e.CardSet)
				.WithOptional(e => e.TicketContainer)
				.HasForeignKey(e => e.TicketContainerId)
				.WillCascadeOnDelete(false);

			this.HasMany(e => e.TicketSet)
				.WithRequired(e => e.TicketContainer)
				.HasForeignKey(e => e.ContainerId);
		}
	}
}
