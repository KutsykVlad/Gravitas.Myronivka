using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class FixedAssetMap : EntityTypeConfiguration<ExternalData.FixedAsset> {

			public FixedAssetMap() {
				this.ToTable("ext.FixedAsset");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.Brand)
					.HasMaxLength(250);

				this.Property(e => e.Model)
					.HasMaxLength(250);

				this.Property(e => e.TypeCode)
					.HasMaxLength(250);

				this.Property(e => e.RegistrationNo)
					.HasMaxLength(250);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);
			}
		}
	}
}