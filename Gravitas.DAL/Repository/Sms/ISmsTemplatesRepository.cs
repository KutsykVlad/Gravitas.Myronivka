using Gravitas.Model;
using Gravitas.Model.DomainModel.Sms.DTO;

namespace Gravitas.DAL 
{
    public interface ISmsTemplatesRepository : IBaseRepository<GravitasDbContext>
    {
        string GetSmsTemplate(long templateId);
        SmsTemplates GetSmsTemplates();
    }
}
