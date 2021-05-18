using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class LabInfectionedClassifierMap : EntityTypeConfiguration<LabInfectionedСlassifier>
    {
        public LabInfectionedClassifierMap()
        {
            ToTable("ext.LabInfectionedСlassifier");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}