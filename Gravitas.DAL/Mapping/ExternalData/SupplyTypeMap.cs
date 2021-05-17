using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class SupplyTypeMap : EntityTypeConfiguration<ExternalData.SupplyType> {

			public SupplyTypeMap()
			{
				this.ToTable("ext.SupplyType");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);
			}
		}
	}
}