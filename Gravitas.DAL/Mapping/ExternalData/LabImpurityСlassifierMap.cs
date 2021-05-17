using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class LabImpurityClassifierMap : EntityTypeConfiguration<ExternalData.LabImpurityСlassifier> {

			public LabImpurityClassifierMap()
			{
				this.ToTable("ext.LabImpurityСlassifier");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);
			}
		}
	}
}