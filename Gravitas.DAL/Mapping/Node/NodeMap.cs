using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping.Node
{
    class NodeMap : EntityTypeConfiguration<Model.DomainModel.Node.DAO.Node>
    {
        public NodeMap()
        {
            ToTable("Node");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);

            HasOptional(e => e.OrganizationUnit)
                .WithMany(e => e.NodeSet)
                .HasForeignKey(e => e.OrganisationUnitId);
        }
    }
}