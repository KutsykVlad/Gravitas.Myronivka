using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.DAL.Mapping
{
    public class EmployeeRolesMap : EntityTypeConfiguration<EmployeeRole>
    {
        public EmployeeRolesMap()
        {
            this.ToTable("EmployeeRoles");

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.Employee)
                .WithMany(p => p.EmployeeRoles)
                .HasForeignKey(p => p.EmployeeId);

            this.HasRequired(p => p.Role)
                .WithMany(p => p.EmployeeRoles)
                .HasForeignKey(p => p.RoleId);
        }
    }
}
