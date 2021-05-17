using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;

namespace Gravitas.DAL.Mapping {

	class DeviceParamMap : EntityTypeConfiguration<DeviceParam> {
		public DeviceParamMap() {
			this.ToTable("DeviceParam");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.HasMany(e => e.DeviceSet)
				.WithOptional(e => e.DeviceParam)
				.HasForeignKey(e => e.ParamId);
		}
	}
}
