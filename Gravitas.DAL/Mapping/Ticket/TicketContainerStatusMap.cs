using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping
{
	class TicketContainerStatusMap : EntityTypeConfiguration<TicketContainerStatus> {

		public TicketContainerStatusMap() {
			this.ToTable("TicketContainerStatus");
			
			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.HasMany(e => e.TicketContainerSet)
				.WithRequired(e => e.TicketContainerStatus)
				.HasForeignKey(e => e.StatusId);
		}
	}
}
