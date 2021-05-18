using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class LabHumidityClassifierMap : EntityTypeConfiguration<LabHumidityСlassifier>
    {
        public LabHumidityClassifierMap()
        {
            ToTable("ext.LabHumidityСlassifier");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}