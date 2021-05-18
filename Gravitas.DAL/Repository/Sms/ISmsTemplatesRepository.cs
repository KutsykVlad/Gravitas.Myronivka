using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Sms.DTO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Sms 
{
    public interface ISmsTemplatesRepository : IBaseRepository
    {
        string GetSmsTemplate(SmsTemplate templateId);
        SmsTemplates GetSmsTemplates();
    }
}
