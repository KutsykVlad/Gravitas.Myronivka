using Gravitas.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.DAL.Mapping
{
    class QueueRegisterMap : BaseEntityMap<QueueRegister, long>
    {
        public QueueRegisterMap()
        {
            this.ToTable("QueueRegister");

            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(e => e.RegisterTime);

            this.Property(e => e.TicketContainerId);

            this.Property(e => e.TruckPlate);

            this.Property(e => e.TrailerPlate);

            this.Property(e => e.PhoneNumber);
        }
    }
}
