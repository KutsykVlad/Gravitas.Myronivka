using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.OwnTransport
{
    class OwnTransportMap : BaseEntityMap<Model.DomainModel.OwnTransport.DAO.OwnTransport, int>
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