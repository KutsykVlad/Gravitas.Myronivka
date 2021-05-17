using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;

namespace Gravitas.DAL.Mapping
{
	class DeviceTypeMap : EntityTypeConfiguration<DeviceType> {
		public DeviceTypeMap() {
			this.ToTable("DeviceType");

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.HasMany(e => e.DeviceSet)
				.WithRequired(e => e.DeviceType)
				.HasForeignKey(e => e.TypeId)
				.WillCascadeOnDelete(false);
		}
	}
}
