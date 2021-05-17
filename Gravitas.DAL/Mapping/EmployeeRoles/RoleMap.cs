using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.DAL.Mapping
{
    public class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.ToTable("Role");

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.Name).
                HasMaxLength(50);
        }
    }
}
