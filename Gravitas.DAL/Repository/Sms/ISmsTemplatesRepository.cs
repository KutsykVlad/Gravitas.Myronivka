using Gravitas.Model;

namespace Gravitas.DAL 
{
    public interface ISmsTemplatesRepository : IBaseRepository<GravitasDbContext>
    {
        string GetSmsTemplate(long templateId);
        SmsTemplates GetSmsTemplates();
    }
}
