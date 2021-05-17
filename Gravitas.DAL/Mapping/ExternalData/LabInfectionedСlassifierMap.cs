using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class LabInfectionedClassifierMap : EntityTypeConfiguration<ExternalData.LabInfectionedСlassifier> {

			public LabInfectionedClassifierMap() {
				this.ToTable("ext.LabInfectionedСlassifier");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);
			}
		}
	}
}