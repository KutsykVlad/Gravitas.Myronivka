using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class ProductMap : EntityTypeConfiguration<ExternalData.Product> {

			public ProductMap() {

				this.ToTable("ext.Product");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.ShortName)
					.HasMaxLength(500);

				this.Property(e => e.FullName)
					.HasMaxLength(500);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);
			}
		}
	}
}