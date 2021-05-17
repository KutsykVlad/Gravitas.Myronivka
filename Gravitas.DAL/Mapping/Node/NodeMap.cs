using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;

namespace Gravitas.DAL.Mapping {

	class NodeMap : EntityTypeConfiguration<Node> {

		public NodeMap() {
			this.ToTable("Node");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);
			this.Property(e => e.Code)
				.IsRequired()
				.HasMaxLength(50);

			this.HasOptional(e => e.OrganizationUnit)
				.WithMany(e => e.NodeSet)
				.HasForeignKey(e => e.OrganisationUnitId);
			
			this.HasMany(e => e.NodeConstraintSet)
				.WithRequired(e => e.Node)
				.WillCascadeOnDelete(false);
		}
	}
}
