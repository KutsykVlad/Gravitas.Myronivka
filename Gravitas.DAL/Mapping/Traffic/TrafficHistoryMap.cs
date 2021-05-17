using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
    class TrafficHistoryMap : EntityTypeConfiguration<TrafficHistory>
    {
        public TrafficHistoryMap()
        {
            this.ToTable("TrafficHistory");
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.TicketContainer)
                .WithMany(p => p.TrafficHistory)
                .HasForeignKey(p => p.TicketContainerId);

            this.HasRequired(p => p.Node)
                .WithMany(p => p.TrafficHistory)
                .HasForeignKey(p => p.NodeId);

            this.Property(p => p.EntranceTime)
                .HasColumnType("datetime2");

            this.Property(p => p.DepartureTime)
                .HasColumnType("datetime2");
        }
    }
}
