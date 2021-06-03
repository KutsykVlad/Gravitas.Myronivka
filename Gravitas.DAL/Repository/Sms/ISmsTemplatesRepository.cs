using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Sms 
{
    public interface ISmsTemplatesRepository
    {
        string GetSmsTemplate(SmsTemplate templateId);
    }
}
