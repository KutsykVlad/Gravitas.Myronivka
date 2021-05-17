using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class OrganizationUnitMap : EntityTypeConfiguration<OrganizationUnit> {
		public OrganizationUnitMap() {
			this.ToTable("OrganizationUnit");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			
			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.HasRequired(e => e.UnitType)
				.WithMany(e => e.OrganizationUnitSet)
				.HasForeignKey(e => e.UnitTypeId);
		}
	}
}
