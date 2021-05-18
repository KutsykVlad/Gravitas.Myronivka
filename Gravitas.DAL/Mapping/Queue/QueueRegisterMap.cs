using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.Queue.DAO;

namespace Gravitas.DAL.Mapping.Queue
{
    class QueueRegisterMap : BaseEntityMap<QueueRegister, int>
    {
        public QueueRegisterMap()
        {
            ToTable("QueueRegister");

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.RegisterTime);

            Property(e => e.TicketContainerId);

            Property(e => e.TruckPlate);

            Property(e => e.TrailerPlate);

            Property(e => e.PhoneNumber);
        }
    }
}
