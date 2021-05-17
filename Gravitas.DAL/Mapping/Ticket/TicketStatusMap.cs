using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping
{
	class TicketStatusMap : EntityTypeConfiguration<TicketStatus> {

		public TicketStatusMap() {
			this.ToTable("TicketStatus");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.HasMany(e => e.TicketSet)
				.WithRequired(e => e.TicketStatus)
				.HasForeignKey(e => e.StatusId);
		}
	}
}
