using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;

namespace Gravitas.DAL.Mapping
{
    class PhoneDictionaryMap : BaseEntityMap<PhoneDictionary, long>
    {
        public PhoneDictionaryMap()
        {
            this.ToTable("PhoneDictionary");
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.Property(t => t.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
