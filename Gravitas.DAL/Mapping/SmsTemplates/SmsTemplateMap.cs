using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Sms.DAO;

namespace Gravitas.DAL.Mapping
{
    class SmsTemplateMap:BaseEntityMap<SmsTemplate, long>
    {
        public SmsTemplateMap()
        {
            this.ToTable("SmsTemplates");
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Text)
                .IsRequired();

        }
    }
}
