using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.PreRegistration.DAO;

namespace Gravitas.DAL.Mapping.PreRegistration
{
    class PreRegisterCompanyMap : BaseEntityMap<PreRegisterCompany, long>
    {
        public PreRegisterCompanyMap()
        {
            ToTable("PreRegisterCompany");
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
