using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class MeasureUnitMap : EntityTypeConfiguration<ExternalData.MeasureUnit> {

			public MeasureUnitMap()
			{
				this.ToTable("ext.MeasureUnit");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.ShortName)
					.HasMaxLength(250);

				this.Property(e => e.FullName)
					.HasMaxLength(250);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);
			}
		}
	}
}