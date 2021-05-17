using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.DAL.Mapping
{
    class TrailerBlackListMap : EntityTypeConfiguration<TrailersBlackListRecord>
    {
        public TrailerBlackListMap()
        {
            this.ToTable("blacklist.Trailer");

            this.HasKey(list => list.Id);
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}
