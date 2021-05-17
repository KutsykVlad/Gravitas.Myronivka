using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.OwnTransport.DAO;

namespace Gravitas.DAL.Mapping
{
    class OwnTransportMap : BaseEntityMap<OwnTransport, long>
    {
        public OwnTransportMap()
        {
            ToTable("OwnTransport");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(e => e.CardId)
                .IsRequired();

            Property(e => e.TruckNo)
                .HasMaxLength(20)
                .IsRequired();
            
            HasIndex(x => x.CardId)
                .IsUnique();
            
            HasIndex(x => x.TruckNo)
                .IsUnique();
        }
    }
}