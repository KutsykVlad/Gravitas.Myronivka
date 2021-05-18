using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.DAL.Mapping.EmployeeRoles
{
    public class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Role");

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .HasMaxLength(50);
        }
    }
}
