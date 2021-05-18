using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.OrganizationUnit.DAO;

namespace Gravitas.DAL.Mapping.OrganizationUnit
{
    class OrganizationUnitTypeMap : EntityTypeConfiguration<OrganizationUnitType>
    {
        public OrganizationUnitTypeMap()
        {
            ToTable("OrganizationUnitType");

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}