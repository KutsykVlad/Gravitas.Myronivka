using System.Collections.Generic;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Connect
{
    public interface IConnectManager
    {
        void SendSms(SmsTemplate templateId, int? ticketId, string phoneNumber = null, Dictionary<string, object> parameters = null, string cardId = null);
        void SendEmail(EmailTemplate templateId, string emailAddress = null, object data = null, string attachmentPath = null, string cardId = null);
        void SendTelegramPost(TelegramGroupType groupType, string message);
    }
}