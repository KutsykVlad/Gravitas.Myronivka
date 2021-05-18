using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.DAL.Mapping.EmployeeRoles
{
    public class AssignmentMap : EntityTypeConfiguration<RoleAssignment>
    {
        public AssignmentMap()
        {
            ToTable("RoleAssignment");

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Role)
                .WithMany(p => p.Assignments)
                .HasForeignKey(p => p.RoleId);

            HasRequired(p => p.Node)
                .WithMany(p => p.Assignments)
                .HasForeignKey(p => p.NodeId);
        }
    }
}