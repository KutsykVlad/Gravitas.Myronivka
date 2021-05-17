using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class ReasonForRefundMap : EntityTypeConfiguration<ExternalData.ReasonForRefund> {

			public ReasonForRefundMap() {

				this.ToTable("ext.ReasonForRefund");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);
			}
		}
	}
}