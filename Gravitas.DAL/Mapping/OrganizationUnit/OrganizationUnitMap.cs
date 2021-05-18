using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping.OrganizationUnit
{
    class OrganizationUnitMap : EntityTypeConfiguration<Model.DomainModel.OrganizationUnit.DAO.OrganizationUnit>
    {
        public OrganizationUnitMap()
        {
            ToTable("OrganizationUnit");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(e => e.UnitType)
                .WithMany(e => e.OrganizationUnitSet)
                .HasForeignKey(e => e.UnitTypeId);
        }
    }
}