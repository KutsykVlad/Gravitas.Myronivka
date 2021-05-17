using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class DeviceMap : EntityTypeConfiguration<Device> {
		public DeviceMap() {
			this.ToTable("Device");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.HasMaxLength(150)
				.IsRequired();

			this.HasOptional(e => e.ParentDevice)
				.WithMany(e => e.ChildDeviceSet)
				.HasForeignKey(e => e.ParentDeviceId);
		}
	}
}
