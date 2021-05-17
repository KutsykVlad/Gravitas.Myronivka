using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

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