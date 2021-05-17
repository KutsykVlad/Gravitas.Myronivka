using Gravitas.Model;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class DeliveryBillTypeMap : EntityTypeConfiguration<ExternalData.DeliveryBillType> {
			public DeliveryBillTypeMap()
			{
				this.ToTable("ext.DeliveryBillType");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);
			}
		}
	}
}