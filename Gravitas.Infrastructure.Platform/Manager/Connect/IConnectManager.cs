using System.Collections.Generic;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Connect
{
    public interface IConnectManager
    {
        bool SendSms(SmsTemplate templateId, int? ticketId, string phoneNumber = null, Dictionary<string, object> parameters = null);
        bool SendEmail(EmailTemplate templateId, string emailAddress = null, object data = null, string attachmentPath = null);
    }
}