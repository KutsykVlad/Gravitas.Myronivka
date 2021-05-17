using Gravitas.Model;
using System.Data.Entity.ModelConfiguration;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

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