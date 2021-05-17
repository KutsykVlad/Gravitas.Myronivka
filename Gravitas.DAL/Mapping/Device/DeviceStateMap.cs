using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class DeviceStateMap : EntityTypeConfiguration<DeviceState> {
		public DeviceStateMap() {
			this.ToTable("DeviceState");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.LastUpdate)
				.HasColumnType("datetime2");

			this.HasMany(e => e.DeviceSet).WithOptional(e => e.DeviceState).HasForeignKey(e => e.StateId);
		}
	}
}
