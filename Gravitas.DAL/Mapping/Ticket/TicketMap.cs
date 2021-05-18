using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping.Ticket
{
    class TicketMap : EntityTypeConfiguration<Model.DomainModel.Ticket.DAO.Ticket>
    {
        public TicketMap()
        {
            ToTable("Ticket");

            HasIndex(e => new
                {
                    e.TicketContainerId,
                    e.OrderNo
                })
                .HasName("IX_ContainerId_OrderNo")
                .IsUnique();

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(e => e.TicketStatus)
                .WithMany(e => e.TicketSet)
                .HasForeignKey(e => e.StatusId);
        }
    }
}