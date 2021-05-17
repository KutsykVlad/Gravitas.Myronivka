using System.Collections.Generic;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface IConnectManager
    {
        bool SendSms(long templateId, long? ticketId, string phoneNumber = null, Dictionary<string, object> parameters = null);
        bool SendEmail(long templateId, string emailAddress = null, object data = null, string attachmentPath = null);
    }
}