using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Device.DAO;

namespace Gravitas.DAL.Mapping.Device
{
    class DeviceStateMap : EntityTypeConfiguration<DeviceState>
    {
        public DeviceStateMap()
        {
            ToTable("DeviceState");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.LastUpdate)
                .HasColumnType("datetime2");

            HasMany(e => e.DeviceSet)
                .WithOptional(e => e.DeviceState)
                .HasForeignKey(e => e.StateId);
        }
    }
}