using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.DAL.Mapping.PackingTare
{
    class TicketPackingTareMap : BaseEntityMap<Model.DomainModel.PackingTare.DAO.TicketPackingTare, long>
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