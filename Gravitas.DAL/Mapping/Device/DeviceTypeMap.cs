using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Device.DAO;

namespace Gravitas.DAL.Mapping.Device
{
    class DeviceTypeMap : EntityTypeConfiguration<DeviceType>
    {
        public DeviceTypeMap()
        {
            ToTable("DeviceType");

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(e => e.DeviceSet)
                .WithRequired(e => e.DeviceType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}