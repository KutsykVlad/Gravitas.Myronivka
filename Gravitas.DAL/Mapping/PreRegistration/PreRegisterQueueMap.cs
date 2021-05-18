using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.PreRegistration.DAO;

namespace Gravitas.DAL.Mapping.PreRegistration
{
    class PreRegisterQueueMap : BaseEntityMap<PreRegisterQueue, int>
    {
        public PreRegisterQueueMap()
        {
            ToTable("PreRegisterQueue");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
