using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.DAL.Mapping.EmployeeRoles
{
    public class EmployeeRolesMap : EntityTypeConfiguration<EmployeeRole>
    {
        public EmployeeRolesMap()
        {
            ToTable("EmployeeRoles");

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Employee)
                .WithMany(p => p.EmployeeRoles)
                .HasForeignKey(p => p.EmployeeId);

            HasRequired(p => p.Role)
                .WithMany(p => p.EmployeeRoles)
                .HasForeignKey(p => p.RoleId);
        }
    }
}