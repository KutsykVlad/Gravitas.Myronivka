using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.PreRegistration.DAO;

namespace Gravitas.DAL.Mapping.PreRegistration
{
    class PreRegisterCompanyMap : BaseEntityMap<PreRegisterCompany, int>
    {
        public PreRegisterCompanyMap()
        {
            ToTable("PreRegisterCompany");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
