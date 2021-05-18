using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping.Ticket
{
    class TicketContainerMap : EntityTypeConfiguration<TicketContainer>
    {
        public TicketContainerMap()
        {
            ToTable("TicketConteiner");

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(e => e.CardSet)
                .WithOptional(e => e.TicketContainer)
                .HasForeignKey(e => e.TicketContainerId)
                .WillCascadeOnDelete(false);

            HasMany(e => e.TicketSet)
                .WithRequired(e => e.TicketContainer)
                .HasForeignKey(e => e.TicketContainerId);
        }
    }
}