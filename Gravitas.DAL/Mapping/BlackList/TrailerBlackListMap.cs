using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping
{
    class TrailerBlackListMap : EntityTypeConfiguration<Gravitas.Model.TrailersBlackListRecord>
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
