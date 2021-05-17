using Gravitas.Model;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class SupplyTransportTypeMap : EntityTypeConfiguration<ExternalData.SupplyTransportType> {
			public SupplyTransportTypeMap() {
				this.ToTable("ext.SupplyTransportType");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);

			}
		}
	}
}