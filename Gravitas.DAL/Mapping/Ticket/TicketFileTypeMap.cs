using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {
    internal class TicketFileTypeMap : EntityTypeConfiguration<TicketFileType> {
        public TicketFileTypeMap()
        {
            ToTable("TicketFileType");

            Property(e => e.Name)
                .IsRequired();
        }
    }
}