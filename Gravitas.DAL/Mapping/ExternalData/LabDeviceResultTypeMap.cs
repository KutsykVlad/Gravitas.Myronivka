using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class LabDeviceResultTypeMap : EntityTypeConfiguration<ExternalData.LabDeviceResultType> {

			public LabDeviceResultTypeMap() {
				this.ToTable("ext.LabDeviceResultType");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);
			}
		}
	}
}