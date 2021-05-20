using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.Sms.DAO;

namespace Gravitas.DAL.Mapping.SmsTemplates
{
    class SmsTemplateMap:BaseEntityMap<SmsTemplate, Model.DomainValue.SmsTemplate>
    {
        public SmsTemplateMap()
        {
            ToTable("SmsTemplates");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Name)
                .HasMaxLength(50);

            Property(t => t.Text)
                .IsRequired();
        }
    }
}
