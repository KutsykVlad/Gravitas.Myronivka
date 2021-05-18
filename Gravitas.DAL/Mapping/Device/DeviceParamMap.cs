using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Device.DAO;

namespace Gravitas.DAL.Mapping.Device
{
    class DeviceParamMap : EntityTypeConfiguration<DeviceParam>
    {
        public DeviceParamMap()
        {
            ToTable("DeviceParam");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasMany(e => e.DeviceSet)
                .WithOptional(e => e.DeviceParam)
                .HasForeignKey(e => e.ParamId);
        }
    }
}