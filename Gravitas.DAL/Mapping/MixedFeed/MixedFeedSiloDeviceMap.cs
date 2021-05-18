using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.DAL.Mapping.MixedFeed
{
    public class MixedFeedSiloDeviceMap : EntityTypeConfiguration<MixedFeedSiloDevice>
    {
        public MixedFeedSiloDeviceMap()
        {
            ToTable("MixedFeedSiloDevice");
            HasKey(e => e.Id);
            
            Property(t => t.DeviceId)
                .IsRequired();
            
            Property(t => t.MixedFeedSiloId)
                .IsRequired();
        }
    }
}