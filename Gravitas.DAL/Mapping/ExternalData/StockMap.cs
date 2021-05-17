using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class StockMap : EntityTypeConfiguration<ExternalData.Stock> {

			public StockMap() {
				this.ToTable("ext.Stock");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.ShortName)
					.HasMaxLength(250);

				this.Property(e => e.FullName)
					.HasMaxLength(250);

				this.Property(e => e.Address)
					.HasMaxLength(250);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);
			}
		}
	}
}