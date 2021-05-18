using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.PreRegistration.DAO;

namespace Gravitas.DAL.Mapping.PreRegistration
{
    class PreRegisterProductMap : BaseEntityMap<PreRegisterProduct, int>
    {
        public PreRegisterProductMap()
        {
            ToTable("PreRegisterProduct");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            HasIndex(x => x.RouteTemplateId)
                .IsUnique();
        }
    }
}
