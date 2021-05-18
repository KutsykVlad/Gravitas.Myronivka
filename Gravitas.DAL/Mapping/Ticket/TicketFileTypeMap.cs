using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.DAL.Mapping.Ticket
{
    internal class TicketFileTypeMap : EntityTypeConfiguration<TicketFileType>
    {
        public TicketFileTypeMap()
        {
            ToTable("TicketFileType");

            Property(e => e.Name)
                .IsRequired();
        }
    }
}