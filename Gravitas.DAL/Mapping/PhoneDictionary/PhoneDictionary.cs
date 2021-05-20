using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Mapping.PhoneDictionary
{
    class PhoneDictionaryMap : BaseEntityMap<Model.DomainModel.PhoneDictionary.DAO.PhoneDictionary, Phone>
    {
        public PhoneDictionaryMap()
        {
            ToTable("PhoneDictionary");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            Property(t => t.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
