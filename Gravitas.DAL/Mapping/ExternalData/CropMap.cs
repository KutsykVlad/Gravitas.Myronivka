using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Crop.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class CropMap : EntityTypeConfiguration<Crop>
    {
        public CropMap()
        {
            ToTable("ext.Crop");

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