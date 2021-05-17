using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.DAL.Mapping.MixedFeed
{
    public class MixedFeedSiloMap : EntityTypeConfiguration<MixedFeedSilo>
    {
        public MixedFeedSiloMap()
        {
            this.ToTable("MixedFeedSilo");
            this.HasKey(e => e.Id);
            
            this.HasOptional(e => e.Product)
                .WithMany(e => e.MixedFeedSiloSet)
                .HasForeignKey(e => e.ProductId);
        }
    }
}