using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class OpVisaMap : EntityTypeConfiguration<OpVisa> {

		public OpVisaMap() {

			this.ToTable("OpVisa");

			this.Property(e => e.DateTime)
				.HasColumnType("datetime2");

		    this.HasOptional(e => e.Employee)
		        .WithMany(e => e.OpVisaSet)
		        .HasForeignKey(e => e.EmployeeId);

		    this.HasRequired(e => e.OpRoutineState)
		        .WithMany(e => e.OpVisaSet)
		        .HasForeignKey(e => e.OpRoutineStateId);
		}
	}
}
