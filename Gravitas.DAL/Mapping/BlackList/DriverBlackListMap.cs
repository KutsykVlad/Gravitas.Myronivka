using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.DAL.Mapping
{
    class DriverBlackListMap : EntityTypeConfiguration<DriversBlackListRecord>
    {
        public DriverBlackListMap()
        {
            this.ToTable("blacklist.Driver");

            this.HasKey(list => list.Id);
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
