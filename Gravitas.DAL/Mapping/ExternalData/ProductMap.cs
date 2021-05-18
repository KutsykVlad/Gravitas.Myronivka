using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("ext.Product");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.ShortName)
                .HasMaxLength(500);

            Property(e => e.FullName)
                .HasMaxLength(500);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}