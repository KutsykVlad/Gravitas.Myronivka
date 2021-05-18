using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.PackingTare
{
    class TicketPackingTareMap : BaseEntityMap<Model.DomainModel.PackingTare.DAO.TicketPackingTare, int>
    {
        public TicketPackingTareMap()
        {
            ToTable("TicketPackingTare");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.PackingTareId)
                .IsRequired();
            
            Property(e => e.TicketId)
                .IsRequired();

            Property(e => e.Count)
                .IsRequired();
        }
    }
}