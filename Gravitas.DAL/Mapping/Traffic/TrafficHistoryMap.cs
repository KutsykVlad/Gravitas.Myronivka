using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Traffic.DAO;

namespace Gravitas.DAL.Mapping.Traffic
{
    class TrafficHistoryMap : EntityTypeConfiguration<TrafficHistory>
    {
        public TrafficHistoryMap()
        {
            ToTable("TrafficHistory");

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.TicketContainer)
                .WithMany(p => p.TrafficHistory)
                .HasForeignKey(p => p.TicketContainerId);

            HasRequired(p => p.Node)
                .WithMany(p => p.TrafficHistory)
                .HasForeignKey(p => p.NodeId);

            Property(p => p.EntranceTime)
                .HasColumnType("datetime2");

            Property(p => p.DepartureTime)
                .HasColumnType("datetime2");
        }
    }
}