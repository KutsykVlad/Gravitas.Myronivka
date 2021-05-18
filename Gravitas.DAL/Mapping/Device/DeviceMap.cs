using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping.Device
{
    class DeviceMap : EntityTypeConfiguration<Model.DomainModel.Device.DAO.Device>
    {
        public DeviceMap()
        {
            ToTable("Device");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            HasOptional(e => e.ParentDevice)
                .WithMany(e => e.ChildDeviceSet)
                .HasForeignKey(e => e.ParentDeviceId);
        }
    }
}