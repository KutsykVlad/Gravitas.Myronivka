using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.DAL.Mapping.MixedFeed
{
    public class MixedFeedSiloDeviceMap : EntityTypeConfiguration<MixedFeedSiloDevice>
    {
        public MixedFeedSiloDeviceMap()
        {
            this.ToTable("MixedFeedSiloDevice");
            this.HasKey(e => e.Id);
            
            this.Property(t => t.DeviceId)
                .IsRequired();
            
            this.Property(t => t.MixedFeedSiloId)
                .IsRequired();
        }
    }
}