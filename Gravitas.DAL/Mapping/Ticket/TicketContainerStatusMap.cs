using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping.Ticket
{
    class TicketContainerStatusMap : EntityTypeConfiguration<TicketContainerStatus>
    {
        public TicketContainerStatusMap()
        {
            ToTable("TicketContainerStatus");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(e => e.TicketContainerSet)
                .WithRequired(e => e.TicketContainerStatus)
                .HasForeignKey(e => e.StatusId);
        }
    }
}