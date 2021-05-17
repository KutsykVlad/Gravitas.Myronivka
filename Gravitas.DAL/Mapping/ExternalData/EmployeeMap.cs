using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class EmployeeMap : EntityTypeConfiguration<ExternalData.Employee> {

			public EmployeeMap() {

				this.ToTable("ext.Employee");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.ShortName)
					.HasMaxLength(250);

				this.Property(e => e.FullName)
					.HasMaxLength(250);

				this.Property(e => e.Position)
					.HasMaxLength(250);

				this.Property(e => e.Email)
					.HasMaxLength(250);

				this.Property(e => e.PhoneNo)
					.HasMaxLength(250);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);

			    this.HasMany(e => e.CardSet)
			        .WithOptional(e => e.Employee)
			        .HasForeignKey(e => e.EmployeeId);
            }
		}
	}
}