using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.PreRegistration.DAO;

namespace Gravitas.DAL.Mapping.PreRegistration
{
    class PreRegisterQueueMap : BaseEntityMap<PreRegisterQueue, long>
    {
        public PreRegisterQueueMap()
        {
            ToTable("PreRegisterQueue");
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
