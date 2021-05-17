using System;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class ContractMap : EntityTypeConfiguration<ExternalData.Contract> {

			public ContractMap() {
				this.ToTable("ext.Contract");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);

				this.Property(e => e.StartDateTime)
					.HasColumnType("datetime2");

				this.Property(e => e.StopDateTime)
					.HasColumnType("datetime2");

				this.Property(e => e.ManagerId)
					.HasMaxLength(250);
			}
		}
	}
}