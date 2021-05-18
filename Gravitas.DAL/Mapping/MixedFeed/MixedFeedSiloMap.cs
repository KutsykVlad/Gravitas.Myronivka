using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.DAL.Mapping.MixedFeed
{
    public class MixedFeedSiloMap : EntityTypeConfiguration<MixedFeedSilo>
    {
        public MixedFeedSiloMap()
        {
            ToTable("MixedFeedSilo");
            HasKey(e => e.Id);
            
            HasOptional(e => e.Product)
                .WithMany(e => e.MixedFeedSiloSet)
                .HasForeignKey(e => e.ProductId);
        }
    }
}