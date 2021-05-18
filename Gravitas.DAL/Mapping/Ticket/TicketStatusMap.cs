using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping.Ticket
{
    class TicketStatusMap : EntityTypeConfiguration<TicketStatus>
    {
        public TicketStatusMap()
        {
            ToTable("TicketStatus");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(e => e.TicketSet)
                .WithRequired(e => e.TicketStatus)
                .HasForeignKey(e => e.StatusId);
        }
    }
}