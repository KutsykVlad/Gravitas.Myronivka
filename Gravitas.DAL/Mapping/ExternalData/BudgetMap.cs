using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Budget.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class BudgetMap : EntityTypeConfiguration<Budget>
    {
        public BudgetMap()
        {
            ToTable("ext.Budget");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}