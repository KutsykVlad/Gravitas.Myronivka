using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.DAL.Mapping.BlackList
{
    class TransportBlackListMap :  EntityTypeConfiguration<TransportBlackListRecord>
    {
        public TransportBlackListMap()
        {
            ToTable("blacklist.Transport");

            HasKey(list => list.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
