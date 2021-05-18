using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class LabImpurityClassifierMap : EntityTypeConfiguration<LabImpurityСlassifier>
    {
        public LabImpurityClassifierMap()
        {
            ToTable("ext.LabImpurityСlassifier");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}