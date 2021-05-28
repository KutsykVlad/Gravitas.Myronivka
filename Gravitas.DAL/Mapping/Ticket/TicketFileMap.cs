using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping.Ticket
{
    internal class TicketFileMap : EntityTypeConfiguration<TicketFile>
    {
        public TicketFileMap()
        {
            ToTable("TicketFile");

            HasRequired(e => e.Ticket)
                .WithMany(e => e.TicketFileSet)
                .HasForeignKey(e => e.TicketId);

            Property(e => e.TypeId)
                .IsRequired();
            
            Property(e => e.Name)
                .IsRequired();

            Property(e => e.DateTime)
                .IsRequired();

            Property(e => e.FilePath)
                .IsRequired();

            Property(e => e.TicketId)
                .IsRequired();
        }
    }
}