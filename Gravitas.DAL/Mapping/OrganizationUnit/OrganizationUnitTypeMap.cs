using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class OrganizationUnitTypeMap : EntityTypeConfiguration<OrganizationUnitType> {

		public OrganizationUnitTypeMap() {

			this.ToTable("OrganizationUnitType");

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);
		}
	}
}