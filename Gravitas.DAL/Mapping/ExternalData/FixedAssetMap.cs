using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class FixedAssetMap : EntityTypeConfiguration<FixedAsset>
    {
        public FixedAssetMap()
        {
            ToTable("ext.FixedAsset");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.Brand)
                .HasMaxLength(250);

            Property(e => e.Model)
                .HasMaxLength(250);

            Property(e => e.TypeCode)
                .HasMaxLength(250);

            Property(e => e.RegistrationNo)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}