using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.DAL.Mapping
{
    public class AssignmentMap: EntityTypeConfiguration<RoleAssignment>
    {
        public AssignmentMap()
        {
            this.ToTable("RoleAssignment");

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.Role)
                .WithMany(p => p.Assignments)
                .HasForeignKey(p => p.RoleId);

            this.HasRequired(p => p.Node)
                .WithMany(p => p.Assignments)
                .HasForeignKey(p => p.NodeId);
        }
    }
}
