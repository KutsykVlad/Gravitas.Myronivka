using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Stock.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class StockMap : EntityTypeConfiguration<Stock>
    {
        public StockMap()
        {
            ToTable("ext.Stock");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.ShortName)
                .HasMaxLength(250);

            Property(e => e.FullName)
                .HasMaxLength(250);

            Property(e => e.Address)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}